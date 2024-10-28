using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipeData", menuName = "Data/Recipe")]
public class Recipe : ScriptableObject
{
    [field: SerializeField] public RecipeIngredient[] Ingredients { get; private set; }
    [field: SerializeField] public float Time { get; private set; }
    [field: SerializeField] public RecipeIngredient Result { get; private set; }
}