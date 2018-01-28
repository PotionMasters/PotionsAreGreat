using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType
{
    BoneClub,
    Bottle,
    BowlTinFoil,
    Cheese,
    Leaf,
    Mushroom,
    OkaySign,
    Ostrich,
    RaccoonFoot,
    TinFoil,
    Turnip,
    Twig,
    TwinSperm
}

public class IngredientsManager : MonoBehaviour
{
    public Ingredient[] prefabs;
    private Dictionary<IngredientType, Ingredient> prefabsDict;


    public Ingredient[] SpawnIngredients(IngredientType[] ingredients)
    {
        Ingredient[] spawns = new Ingredient[ingredients.Length];
        for (int i = 0; i < ingredients.Length; ++i)
        {
            spawns[i] = Instantiate(prefabsDict[ingredients[i]]);
        }
        return spawns;
    }

    public IngredientType[] GetRandomIngredients(int n)
    {
        IngredientType[] types = new IngredientType[n];
        for (int i = 0; i < n; ++i)
        {
            types[i] = RandomEnum<IngredientType>();
        }
        return types;
    }


    private void Awake()
    {
        CreatePrefabDict();
    }
    
    private void CreatePrefabDict()
    {
        prefabsDict = new Dictionary<IngredientType, Ingredient>();
        foreach (Ingredient prefab in prefabs)
        {
            if (prefabsDict.ContainsKey(prefab.Type))
            {
                Debug.LogError("Duplicate prefabs for ingredient type " + prefab.Type.ToString());
            }
            prefabsDict[prefab.Type] = prefab;
        }
        if (prefabsDict.Keys.Count != EnumLength(typeof(IngredientType)))
        {
            Debug.LogError("Missing ingredient prefabs");
        }
    }

    private static T RandomEnum<T>()
    {
        System.Array v = System.Enum.GetValues(typeof(T));
        return (T)v.GetValue(Random.Range(0, v.Length));
    }
    private static int EnumLength(System.Type enum_type)
    {
        return System.Enum.GetValues(enum_type).Length;
    }
}

public class Recipe
{
    public IngredientType[] Ingredients { get; private set; }

    public static Recipe Random(int n, IngredientsManager mgr)
    {
        Recipe recipe = new Recipe();
        recipe.Ingredients = mgr.GetRandomIngredients(n);
        return recipe;
    }
}