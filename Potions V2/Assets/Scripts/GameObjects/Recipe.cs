using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour {

    [SerializeField] List<Ingredient> requiredIngredients;

    public List<Ingredient> RequiredIngredients
    {
        get
        {
            return requiredIngredients;
        }

        set
        {
            requiredIngredients = value;
        }
    }
}
