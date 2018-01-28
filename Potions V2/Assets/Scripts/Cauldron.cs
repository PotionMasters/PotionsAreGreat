using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour {

    [SerializeField]
    private GameObject waterSplash;
    [SerializeField]
    public Transform waterPoint;

    [SerializeField] List<Ingredient> heldIngredients;
    private Recipe currentRecipe;
    
    [Header("Cauldron Properties")]
    [SerializeField] int heat = 100;


    private void Awake()
    {
        currentRecipe = FindObjectOfType<Recipe>();
        if (currentRecipe == null)
        {
            Debug.LogError("Missing recipe");
        }
    }

    public List<Ingredient> HeldIngredients
    {
        get
        {
            return heldIngredients;
        }

        set
        {
            heldIngredients = value;
        }
    }
	
	void Update ()
    {
        if(currentRecipe.Ingredients.Count == 0)
        {
            Debug.Log("You win!");
            //AudioManager.instance.PlaySound2D("Won");
        }	
	}

    public void StirPot()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var ingred = collision.GetComponent<Ingredient>();
        var effects = collision.GetComponent<DraggableObject>();

        heldIngredients.Add(ingred);

        if (currentRecipe.Ingredients.Contains(ingred))
        {
            currentRecipe.Ingredients.Remove(ingred);
            effects.DestroyEffect();
            
            Destroy(ingred.gameObject);

            AudioManager.instance.PlaySound2D("Dropped in Cauldron");

            if (waterSplash !=null)
            {
                GameObject clone = Instantiate(waterSplash, waterPoint);
            }

            Debug.Log(ingred.IngredientName + " has been added.");
        }
    }
}
