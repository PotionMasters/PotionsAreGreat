using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum PanelType { Menu, Book, Ball1, Switch, Ball2, Cauldron, End }
public enum Difficulty { Beginner, Normal, Master }

public class GameManager : MonoBehaviour
{
    private int[] timeLimitsBeginner = new int[7] { 0, 20, 20, 0, 20, 20, 0 };
    private int[] timeLimitsNormal = new int[7] { 0, 10, 10, 0, 10, 10, 0 };
    private int[] timeLimitsMaster = new int[7] { 0, 5, 5, 0, 5, 5, 0 };
    private Difficulty difficulty;

    public Panel[] panels;
    public Recipe GoalRecipe { get; private set; }

    bool player1MusicStart = false;
    bool switchMusic = false;
    bool player2MusicStart = false;
    bool endMusic = false;

    private Coroutine transition;

    public LineDrawer lineDrawer;
    public Transform crystalBall2;
    public Cauldron cauldron;

    private PanelType panelType;
    private float panelStartTime;
    public bool GameWon { get; private set; }
    public bool GameOver { get; private set; }

    MusicManager theMusic;

    public System.Action<Transform, Transform, float> onTransition; // panel0, panel1, t
    public System.Action<bool> onGameOver; // won


    public float GetTimeLeft()
    {
        float limit =
            difficulty == Difficulty.Beginner ? timeLimitsBeginner[(int)panelType] :
            difficulty == Difficulty.Normal ? timeLimitsNormal[(int)panelType] :
            difficulty == Difficulty.Master ? timeLimitsMaster[(int)panelType] : 0;

        return Mathf.Max(0, limit - (Time.timeSinceLevelLoad - panelStartTime));
    }

    private void Awake()
    {
        GameWon = false;
        GameOver = false;
        panelType = PanelType.Menu;
        GoalRecipe = Recipe.Random(3, FindObjectOfType<IngredientsManager>());
        theMusic = FindObjectOfType<MusicManager>();

        Debug.Log("GOAL = " + GoalRecipe.Ingredients[0] + ", " + GoalRecipe.Ingredients[1] + ", " + GoalRecipe.Ingredients[2]);
    }

    private void Start()
    {
        cauldron.onIngredientAdded += OnCauldronAddIngredient;
    }

    private void Update()
    {
        if (panelType != PanelType.Menu && panelType != PanelType.End && panelType != PanelType.Switch)
        {
            if (GetTimeLeft() <= 0)
            {
                if (panelType == PanelType.Cauldron && !GameOver)
                {
                    // Lose by timeout
                    GG(false);
                }
                else
                {
                    AdvancePanel();
                }
            }
        }

        if (panelType == PanelType.Menu)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                difficulty = Difficulty.Beginner;
                AdvancePanel();
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                difficulty = Difficulty.Normal;
                AdvancePanel();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                difficulty = Difficulty.Master;
                AdvancePanel();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            AdvancePanel();
        }

        // Timer
        Text timerText = panels[(int)panelType].timerText;
        if (timerText != null)
        {
            int seconds = Mathf.CeilToInt(GetTimeLeft());
            timerText.text = seconds > 0 ? seconds.ToString() : "";
        }
    }

    private void HandleMusic()
    {
        if (panelType == PanelType.Book && !player1MusicStart)
        {
            AudioManager.instance.PlayMusic(theMusic.player1Music);
            player1MusicStart = true;
            if (endMusic)
            {
                endMusic = false;
            }

        }
        if (panelType == PanelType.Switch && !switchMusic)
        {
            AudioManager.instance.PlayMusic(theMusic.switchMusic);
            switchMusic = true;
        }
        if (panelType == PanelType.Ball2 && !player2MusicStart)
        {
            AudioManager.instance.PlayMusic(theMusic.player2Music);
            player2MusicStart = true;
        }

        if (panelType == PanelType.End && !endMusic)
        {
            AudioManager.instance.PlayMusic(theMusic.endMusic);
            endMusic = true;

            player1MusicStart = false;
            player2MusicStart = false;
            switchMusic = false;

        }
    }

    private void AdvancePanel()
    {
        if (panelType == PanelType.End)
        {
            SceneManager.LoadScene(0);
        }
        else if (panelType == PanelType.Cauldron)
        {
            SetPanelType(panelType + 1, 2.0f, 2.0f);
        }
        else
        {
            SetPanelType(panelType + 1);
        }
    }

    private void SetPanelType(PanelType PanelType, float duration = 0.3f, float delay = 0)
    {
        if (transition == null)
        {
            AudioManager.instance.PlaySound2D("SlideTransition");
            transition = StartCoroutine(TransitionRoutine(PanelType, duration, delay));
        }
    }

    private IEnumerator TransitionRoutine(PanelType newPanelType, float duration, float delay)
    {
        yield return new WaitForSeconds(delay);

        Transform panel0 = panels[(int)panelType].transform;
        Transform panel1 = panels[(int)newPanelType].transform;

        PanelType oldPanelType = panelType;

        panelType = newPanelType;
        panel1.gameObject.SetActive(true);
        panelStartTime = Time.timeSinceLevelLoad;

        duration = Mathf.Max(duration, 0.01f);
        for (float t = 0; ; t += Time.deltaTime / duration)
        {
            t = Mathf.Min(t, 1);
            float tt = Mathf.SmoothStep(0, 1, t);

            Vector3 pos = Camera.main.transform.position;
            float z = pos.z;
            pos = Vector3.Lerp(panel0.position, panel1.position, tt);
            pos.z = z;
            Camera.main.transform.position = pos;

            if (onTransition != null)
            {
                onTransition(panel0, panel1, tt);
            }

            yield return null;

            if (t >= 1)
            {
                break;
            }
        }
        HandleMusic();

        OnTransitionDone(oldPanelType, newPanelType);
        panel0.gameObject.SetActive(false);
        transition = null;
    }

    private void OnTransitionDone(PanelType oldPanel, PanelType newPanel)
    {
        if (newPanel == PanelType.Switch)
        {
            // Move Line drawing to other crystal ball
            Vector3 oldPos = lineDrawer.transform.position;
            lineDrawer.transform.SetParent(crystalBall2.transform, false);
            lineDrawer.Move(lineDrawer.transform.position - oldPos);
        }
    }

    private void OnCauldronAddIngredient(IngredientType ingredient)
    {
        if (!cauldron.IsPotionCorrectSoFar(GoalRecipe))
        {
            GG(false);
        }
        else if (cauldron.IsPotionCorrect(GoalRecipe))
        {
            GG(true);
        }
    }

    private void GG(bool won)
    {
        if (won)
        {
            Debug.Log("WIN");
        }
        else
        {
            Debug.Log("FAIL (incorrect ingredients)");

        }
        GameWon = won;
        if (onGameOver != null)
        {
            onGameOver(won);
        }

        GameOver = true;

        AdvancePanel();
    }
}
