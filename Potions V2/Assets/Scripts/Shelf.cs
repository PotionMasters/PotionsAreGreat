using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    private const int n = 16;
    public Transform[] spawnPointsLeft;

    private void Start()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        IngredientsManager ingredMgr = FindObjectOfType<IngredientsManager>();

        List<Ingredient> spawns = new List<Ingredient>();
        spawns.AddRange(ingredMgr.SpawnIngredients(gm.GoalRecipe.Ingredients));
        spawns.AddRange(ingredMgr.SpawnIngredients(ingredMgr.GetRandomIngredients(n - spawns.Count)));
        Ingredient[] shuffled = ShuffleArray(spawns.ToArray());

        for (int i = 0; i < n / 2; ++i)
        {
            shuffled[i].transform.position = spawnPointsLeft[0].position + Vector3.right * (3 * i);
        }
        for (int i = 0; i < n / 2; ++i)
        {
            shuffled[i + (n / 2)].transform.position = spawnPointsLeft[1].position + Vector3.right * (3 * i);
        }
    }

    public static T[] ShuffleArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length; ++i)
        {
            int r = UnityEngine.Random.Range(0, array.Length - 1);
            T temp = array[r];
            array[r] = array[i];
            array[i] = temp;
        }
        return array;
    }
}
