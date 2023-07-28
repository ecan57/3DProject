using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/RecipeList")]
public class RecipeListSO : ScriptableObject
{
    public List<RecipeSO> recipeSOList;
}
