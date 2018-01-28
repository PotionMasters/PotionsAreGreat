using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
    public Transform winParent, loseParent;

    public void Show()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm.GameWon)
        {
            winParent.gameObject.SetActive(true);
        }
        else
        {
            loseParent.gameObject.SetActive(true);
        }

    }
}
