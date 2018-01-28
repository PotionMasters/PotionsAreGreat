using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    private GameObject waterSplash;
    public Transform waterPoint;

    [SerializeField] List<IngredientType> heldIngredients;
    public List<IngredientType> HeldIngredients { get; private set; }

    [Header("Cauldron Properties")]
    [SerializeField] int heat = 100;

    public System.Action<IngredientType> onIngredientAdded;


    

    public bool IsPotionCorrect(Recipe recipe)
    {
        if (heldIngredients.Count != recipe.Ingredients.Length)
        {
            return false;
        }

        for (int i = 0; i < recipe.Ingredients.Length; ++i)
        {
            if (recipe.Ingredients[i] != heldIngredients[i])
            {
                return false;
            }
        }
        return true;
    }

    public bool IsPotionCorrectSoFar(Recipe recipe)
    {
        if (heldIngredients.Count > recipe.Ingredients.Length)
        {
            return false;
        }

        for (int i = 0; i < heldIngredients.Count; ++i)
        {
            if (heldIngredients[i] != recipe.Ingredients[i])
            {
                return false;
            }
        }
        return true;
    }

    public void StirPot()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var ingredient = collider.GetComponent<Ingredient>();
        //var effects = collider.GetComponent<DraggableObject>();

        if (ingredient != null)
        {
            AudioManager.instance.PlaySound2D("Dropped in Cauldron");
            AddIngredient(ingredient.Type);
            Splash();
            Destroy(ingredient.gameObject);
            //effects.DestroyEffect();
        }
    }

    private void AddIngredient(IngredientType ingredient)
    {
        heldIngredients.Add(ingredient);
        if (onIngredientAdded != null)
        {
            onIngredientAdded(ingredient);
        }
    }
    private void Splash()
    {
        if (waterSplash != null)
        {
            Instantiate(waterSplash, waterPoint);
        }
    }
}
