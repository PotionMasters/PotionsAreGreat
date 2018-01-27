using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RecipeScene))]
public class RecipeSceneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RecipeScene sceneScript = (RecipeScene)target;
        if(GUILayout.Button("Populate Recipe Book"))
        {
            sceneScript.ClearBook();
            sceneScript.PopulateBook();
        }
    }
}
