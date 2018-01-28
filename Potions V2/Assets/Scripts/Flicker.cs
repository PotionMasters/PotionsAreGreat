using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    private SpriteRenderer sprite;
    private float a;
    public float intensity = 0.2f;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        a = sprite.color.a;
    }
    private void Update()
    {
        Color c = sprite.color;
        c.a = a + Mathf.PerlinNoise(Time.timeSinceLevelLoad * 15f, 0) * intensity;
        sprite.color = c;
    }

}
