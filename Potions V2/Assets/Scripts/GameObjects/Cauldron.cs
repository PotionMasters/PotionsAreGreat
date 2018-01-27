using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour {

    [SerializeField] List<Ingredient> heldIngredients;
    [SerializeField] Recipe currentRecipe;

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
	
	// Update is called once per frame
	void Update ()
    {
        if(currentRecipe.RequiredIngredients.Count == 0)
        {
            Debug.Log("You win!");
            AudioManager.instance.PlaySound2D("Won");
        }	
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var ingred = collision.GetComponent<Ingredient>();

        heldIngredients.Add(ingred);

        if (currentRecipe.RequiredIngredients.Contains(ingred))
        {
            currentRecipe.RequiredIngredients.Remove(ingred);
            Destroy(ingred.gameObject);
            AudioManager.instance.PlaySound("Dropped in Cauldron", this.transform.position);
            Debug.Log(ingred.IngredientName + " has been added.");
        }
    }
}
