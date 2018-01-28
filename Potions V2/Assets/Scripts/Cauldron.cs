using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [SerializeField]
    private GameObject waterSplash;
    public Transform waterPoint;
    public SpriteRenderer liquidSprite;
    public SpriteRenderer roomLightSprite;

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
       var effects = collider.GetComponent<DraggableObject>();

        if (ingredient != null)
        {
            AudioManager.instance.PlaySound2D("Dropped in Cauldron");
            AddIngredient(ingredient);
            Splash();
            effects.DestroyEffect();

            Destroy(ingredient.gameObject);
        }
    }

    private void AddIngredient(Ingredient ingredient)
    {
        heldIngredients.Add(ingredient.Type);
        if (onIngredientAdded != null)
        {
            onIngredientAdded(ingredient.Type);
            StartCoroutine(LiquidColorRoutine(ingredient.cauldronColor));
        }
    }

    private void Splash()
    {
        if (waterSplash != null)
        {
            GameObject clone = Instantiate(waterSplash, waterPoint.transform.position, waterSplash.transform.rotation);
        }
    }

    private IEnumerator LiquidColorRoutine(Color color)
    {
        Color c0 = liquidSprite.color;
        for (float t = 0; t < 1; t += Time.deltaTime / 0.3f)
        {
            liquidSprite.color = Color.Lerp(c0, color, t);
            Color roomTint = liquidSprite.color;
            roomTint.a = roomLightSprite.color.a;
            roomLightSprite.color = roomTint;
            yield return null;
        }
    }
}
