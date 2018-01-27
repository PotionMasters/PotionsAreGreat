using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour {

    public struct RecipeStep
    {
        public Ingredient ingredient;
        public bool Stir;
        public int Heat;
    }

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
