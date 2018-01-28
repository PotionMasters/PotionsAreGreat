using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamWobble : MonoBehaviour
{
    private void Update()
    {
        //float p = Mathf.PerlinNoise(Time.timeSinceLevelLoad * 0.2f, 0);
        float mod = (Mathf.Sin(Time.timeSinceLevelLoad * 0.5f) + 1) / 2;
        Camera.main.orthographicSize = 9 - mod * 0.2f;
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.timeSinceLevelLoad * 0.3f) * 0.5f);
    }
}
