using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeScene : MonoBehaviour
{
    [SerializeField] private Recipe recipe;
    [SerializeField] private GameObject book;
    [SerializeField] private List<Transform> steps;

    public void PopulateBook()
    {
        if(recipe != null)
        {
            int i = 0;
            foreach (var ingred in recipe.RequiredIngredients)
            {
                if(i < steps.Count) Instantiate(ingred, steps[i++].transform, false);
            }
        }
    }

    public void ClearBook()
    {
       // foreach(Transform child in steps) { DestroyImmediate(child.gameObject); }         
    }
}
