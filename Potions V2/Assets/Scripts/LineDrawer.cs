using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public AnimationCurve falloff;
    public float falloffRadius = 2;
    public LineRenderer linePrefab;

    private List<LineRenderer> lines = new List<LineRenderer>();
    private Vector2 lastVert;

    private bool drawing = false;
    private int lineIndex = -1;

    private const float vertDistance = 0.25f;
    private const int maxVerts = 500;

    private void Update()
    {
        bool mouseDown = Input.GetKey(KeyCode.Mouse0);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    
        if (mouseDown)
        {
            if (!drawing)
            {
                // Start Drawing
                drawing = true;

                LineRenderer line = Instantiate(linePrefab);
                line.transform.SetParent(transform);
                lines.Add(line);
                line.positionCount = 0;
                ++lineIndex;

                PlaceVertex(mousePos);
            }
            else if (Vector2.Distance(lastVert, mousePos) > vertDistance)
            {
                // Continue Drawing
                PlaceVertex(mousePos);
            }
        }
        else if (drawing)
        {
            // Stop drawing
            drawing = false;
        }

        foreach (LineRenderer line in lines)
        {
            UpdateLineWidth(line);
        }
    }
    private void PlaceVertex(Vector2 pos)
    {
        if (GetTotalVerts() < maxVerts)
        {
            int i = lines[lineIndex].positionCount;
            lines[lineIndex].positionCount += 1;
            lines[lineIndex].SetPosition(i, pos);
            lastVert = pos;
        }
    }

    private void UpdateLineWidth(LineRenderer line)
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

    private int GetTotalVerts()
    {
        int count = 0;
        foreach (LineRenderer line in lines)
        {
            count += line.positionCount;
        }
        return count;
    }
}
