using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] IngredientType type;
    public IngredientType Type
    {
        get
        {
            return type;
        }
    }

    [SerializeField] List<Ingredient> compatibleIngredients;
}
