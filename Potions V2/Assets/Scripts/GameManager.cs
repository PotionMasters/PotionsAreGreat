using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum PanelType { Menu, Book, Ball1, Switch, Ball2, Cauldron, End }

public class GameManager : MonoBehaviour
{
    public Panel[] panels;
    public int[] panelTimeLimitss;
    public Recipe GoalRecipe { get; private set; }

    bool player1MusicStart = false;
    bool switchMusic = false;
    bool player2MusicStart = false;
    bool endMusic = false;


    public LineDrawer lineDrawer;
    public Transform crystalBall2;
    public Cauldron cauldron;

    private PanelType panelType;
    private float panelStartTime;
    public bool GameWon { get; private set; }

    MusicManager theMusic;

    public System.Action<Transform, Transform, float> onTransition; // panel0, panel1, t


    public float GetTimeLeft()
    {
        return Mathf.Max(0, panelTimeLimitss[(int)panelType] - (Time.timeSinceLevelLoad - panelStartTime));
    }

    private void Awake()
    {
        GameWon = false;
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
                AdvancePanel();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //HandleMusic();

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
        // HACK
        if (panelType == PanelType.Cauldron)
        {
            panels[panels.Length - 1].GetComponent<EndPanel>().Show();
            SetPanelType(panelType + 1, 2.5f);
            return;
        }

        if (panelType == PanelType.End)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SetPanelType(panelType + 1);
        }
    }

    private void SetPanelType(PanelType PanelType, float duration = 0.3f)
    {
        StartCoroutine(TransitionRoutine(PanelType, duration));
    }

    private IEnumerator TransitionRoutine(PanelType newPanelType, float duration)
    {
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
            // Fail
            Debug.Log("FAIL (incorrect ingredients)");
            AdvancePanel();
        }
        else if (cauldron.IsPotionCorrect(GoalRecipe))
        {
            // Win
            Debug.Log("WIN");
            GameWon = true;
            AdvancePanel();
        }
    }
}
