using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour {

    [SerializeField] string ingredientName;

    public string IngredientName
    {
        get
        {
            return ingredientName;
        }
    }
}
