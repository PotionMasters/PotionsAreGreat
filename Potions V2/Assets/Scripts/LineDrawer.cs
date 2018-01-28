using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public AnimationCurve falloff;
    public float falloffRadius = 2;

    private LineRenderer line;
    private Vector2 lastVert;
    private bool drawing = false;
    private const float vertDistance = 0.25f;
    private const int maxVerts = 300;

    public AnimationCurve curve;


    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;
    }
    private void Update()
    {
        bool mouseDown = Input.GetKey(KeyCode.Mouse0);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    
        if (mouseDown)
        {
            if (!drawing)
            {
                line.positionCount = 0;
                drawing = true;
                PlaceVertex(mousePos);
            }
            else if (Vector2.Distance(lastVert, mousePos) > vertDistance)
            {
                PlaceVertex(mousePos);
            }
        }
        else if (drawing)
        {
            drawing = false;
        }

        UpdateLineWidth();
    }
    private void PlaceVertex(Vector2 pos)
    {
        if (line.positionCount < maxVerts)
        {
            int i = line.positionCount;
            line.positionCount += 1;
            line.SetPosition(i, pos);
        }
    }

    private void UpdateLineWidth()
    {
        Keyframe[] keys = new Keyframe[line.positionCount];
        for (int i = 0; i < keys.Length; ++i)
        {
            float dist = Vector2.Distance(line.GetPosition(i), transform.position);
            float t = 1 - Mathf.Min(dist / falloffRadius, 1);

            float i_float = (float)i / keys.Length;
            float mult = Mathf.Lerp(0.8f, 1.0f, (Mathf.Sin(Time.time * 30.0f) + 1.0f) / 2.0f);
            keys[i] = new Keyframe(i_float, falloff.Evaluate(t) * mult);
        }
        line.widthCurve = new AnimationCurve(keys);
    }
}
