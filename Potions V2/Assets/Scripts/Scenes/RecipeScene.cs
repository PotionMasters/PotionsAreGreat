using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeScene : MonoBehaviour
{
    [SerializeField] private Recipe recipe;
    [SerializeField] private GameObject book;
    [SerializeField] private List<Transform> steps;

    private bool bookPopulated = false;

    private void Update()
    {
        if(bookPopulated == false && recipe != null)
        {
            PopulateBook();
            bookPopulated = true;
        }
    }

    public void PopulateBook()
    {
        if(recipe != null)
        {
            foreach (var ingred in recipe.RequiredIngredients)
            {
                Instantiate(ingred, Vector3.zero, Quaternion.identity, book.transform);
            }
        }
    }

    public void ClearBook()
    {
       foreach(Transform child in steps) { DestroyImmediate(child.gameObject); }         
    }
}
