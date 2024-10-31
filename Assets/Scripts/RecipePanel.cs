using UnityEngine;

public class RecipePanel : Panel
{
    [SerializeField] private TopBar _topBar;

    public void SetRecipe(Recipe recipe)
    {
        base.Open();

        _topBar.SetTitleText($"{recipe.Result.ItemData.Name} recipe");
    }
}