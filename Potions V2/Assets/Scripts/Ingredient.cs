using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour {

    [SerializeField] string ingredientName;
    [SerializeField] List<Ingredient> compatibleIngredients;

    public string IngredientName
    {
        get
        {
            return ingredientName;
        }
    }
}
