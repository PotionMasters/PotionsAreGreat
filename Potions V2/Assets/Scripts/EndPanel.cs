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


    private void Awake()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        gm.onGameOver += OnGameOver;
    }

    private void OnGameOver(bool won)
    {
        if (won)
        {
            winParent.gameObject.SetActive(true);
        }
        else
        {
            loseParent.gameObject.SetActive(true);
            FailEffect();
        }
        StartCoroutine(FailScreenInEffect());
    }

    private void FailEffect()
    {
        if (failurePotionFX != null && deathFXPoint != null)
        {
            GameObject clone = Instantiate(failurePotionFX, deathFXPoint.position, failurePotionFX.transform.rotation);
            AudioManager.instance.PlaySound2D("Explosion");
        }
    }

    private IEnumerator FailScreenInEffect()
    {
        yield return new WaitForSeconds(3);
        AudioManager.instance.PlaySound2D("BreakGlass");
    }
}
