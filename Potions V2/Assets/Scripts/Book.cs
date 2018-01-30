using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    //[SerializeField] private Recipe recipe;
    //[SerializeField] private List<Ingredient> requiredIngredients;
    //[SerializeField] private float buffer = 1.0f;
    //float currentOffset = 0;

    private const float baseOffset = 2;

    private void Start()
    {
        //need a way to figure out how to tell the player which is the first ingredient and which is the last

        GameManager gm = FindObjectOfType<GameManager>();
        IngredientsManager ingredMgr = FindObjectOfType<IngredientsManager>();

        Ingredient[] spawns = ingredMgr.SpawnIngredients(gm.GoalRecipe.Ingredients);
        for (int i = 0; i < spawns.Length; ++i)
        {
            int j = spawns.Length - 1 - i;
            spawns[j].transform.position = transform.position + Vector3.up * (4 * i + baseOffset);
            spawns[j].GetComponent<DraggableObject>().draggable = false;
            spawns[j].GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    //public void PopulateIngredientsList()
    //{
    //    requiredIngredients.Clear();
    //    int listRange = Random.Range(2, 4);

    //    for(int i=0; i < listRange; i++)
    //    {
    //        requiredIngredients.Add(recipe.Ingredients[Random.Range(0, recipe.Ingredients.Count)]);
    //    }
    //}

    //public void PopulateBook()
    //{
    //    foreach(var i in requiredIngredients)
    //    {
    //        Vector3 pos = new Vector3
    //        {
    //            x = transform.position.x,
    //            y = transform.position.y + i.GetComponentInChildren<SpriteRenderer>().bounds.size.y + buffer + currentOffset,
    //            z = transform.position.z
    //        };

    //        var currentIng = Instantiate(i, pos, Quaternion.identity,transform);
    //        currentIng.tag = "SPAWNED_OBJECT";

    //        currentOffset += Mathf.Abs(pos.y);
    //    }
    //}

    //public void ClearIngredients()
    //{
    //    currentOffset = 0;

    //    List<GameObject> tempList = new List<GameObject>();
    //    foreach(Transform i in transform)
    //    {
    //        if (i.tag == "SPAWNED_OBJECT") { tempList.Add(i.gameObject); }
    //    }

    //    foreach (var i in tempList) { DestroyImmediate(i); }
    //}
}
