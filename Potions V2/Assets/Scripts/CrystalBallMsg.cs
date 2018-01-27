using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrystalBallMsg : MonoBehaviour
{
    public TextMesh mesh;

    public void Awake()
    {
        mesh = GetComponent<TextMesh>();
        StartCoroutine(Routine());
    }

    public IEnumerator Routine()
    {
        Color c;

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            c = Color.Lerp(Color.clear, Color.white, t);
            mesh.color = c;
            yield return null;
        }

        yield return new WaitForSeconds(3);

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            c = Color.Lerp(Color.white, Color.clear, t);
            mesh.color = c;
            yield return null;
        }

        Destroy(gameObject);
    }
}

