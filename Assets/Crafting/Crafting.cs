using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{

    [System.Serializable]
    public class RecipeData
    {

        public int[] IngredientIDs;
        public int[] IngredientAmount;
        public int ResultID;

    }

    public RecipeData[] Recipes;
    public GameObject RecipePrefab;
    public Transform RecipeContainer;


    private void Start()
    {
        for (int i = 0; i < Recipes.Length; i++)
        {
            GameObject obj = Instantiate(RecipePrefab, RecipeContainer);
            Recipe recipe = obj.GetComponent<Recipe>();
            recipe.SetRecipe(Recipes[i]);
        }
    }

}
