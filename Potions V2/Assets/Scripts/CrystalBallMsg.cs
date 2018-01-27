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
        Color c = Color.white; 

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            c.a = Mathf.Lerp(0, 1, t);
            mesh.color = c;
            yield return null;
        }

        yield return new WaitForSeconds(3);

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            c.a = Mathf.Lerp(1, 0, t);
            mesh.color = c;
            yield return null;
        }

        Destroy(gameObject);
    }
}

