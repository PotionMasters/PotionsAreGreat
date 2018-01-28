using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Book))]
public class RecipeSceneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Book sceneScript = (Book)target;
        if(GUILayout.Button("Populate Ingredients List"))
        {
            sceneScript.PopulateIngredientsList();
        }

        if (GUILayout.Button("Populate Recipe Book"))
        {
            sceneScript.ClearIngredients();
            sceneScript.PopulateBook();
        }

        if(GUILayout.Button("Clear Ingredients"))
        {
            sceneScript.ClearIngredients();
        }
    }
}
