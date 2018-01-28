using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum Panel { Menu, Book, Ball1, Switch, Ball2, Cauldron, End }

public class GameManager : MonoBehaviour
{
    public GameObject[] panelEntities;
    public int[] panelTimeLimits;

    private Panel panel;
    private float panelStartTime;


    public float GetTimeLeft()
    {
        return Mathf.Max(0, panelTimeLimits[(int)panel] - (Time.timeSinceLevelLoad - panelStartTime));
    }

    private void Start()
    {
        panel = Panel.Menu;
    }

    private void Update()
    {
        if (panel != Panel.Menu && panel != Panel.End && panel != Panel.Switch )
        {
            if (GetTimeLeft() <= 0)
            {
                AdvancePanel();
            }
        }
        else if (panel == Panel.Menu || panel == Panel.Switch)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AdvancePanel();
            }
        }
        else if (panel == Panel.End)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetPanel(Panel.Menu, true);
            }
        }
    }

    private void AdvancePanel()
    {
        SetPanel(panel + 1);
    }

    private void SetPanel(Panel panel, bool immediate=false)
    {
        StartCoroutine(TransitionRoutine(panel, immediate));
    }

    private IEnumerator TransitionRoutine(Panel newPanel, bool immediate=false)
    {
        Transform panel0 = panelEntities[(int)panel].transform;
        Transform panel1 = panelEntities[(int)newPanel].transform;

        panel = newPanel;
        panel1.gameObject.SetActive(false);
        panelStartTime = Time.timeSinceLevelLoad;

        float duration = immediate ? 0 : 1;
        for (float t = 0; ; t += Time.deltaTime / duration)
        {
            t = Mathf.Min(t, 1);
            float tt = Mathf.Pow(t, 2);

            Camera.main.transform.position = Vector3.Lerp(panel0.position, panel1.position, tt);

            yield return null;

            if (t >= 1)
            {
                break;
            }
        }

        panel0.gameObject.SetActive(false);
    }

}
