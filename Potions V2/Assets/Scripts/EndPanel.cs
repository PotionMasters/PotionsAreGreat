using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject failurePotionFX;
    [SerializeField]
    private Transform deathFXPoint;

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

        StartCoroutine(EffectRoutine());
    }

    private IEnumerator EffectRoutine()
    {
        yield return new WaitForSeconds(1);

        if (failurePotionFX != null && deathFXPoint != null)
        {
            GameObject clone = Instantiate(failurePotionFX, deathFXPoint.position, failurePotionFX.transform.rotation);
            // effect

        }
    }
}
