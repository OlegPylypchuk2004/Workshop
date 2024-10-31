using UnityEngine;

public class RecipesListPanel : Panel
{
    [SerializeField] private RecipesTab _recipesTab;
    [SerializeField] private ItemRecipePanel[] _itemRecipePanels;

    public override void Open()
    {
        base.Open();

        Recipe[] _recipes = _recipesTab.Recipes;

        for (int i = 0; i < _itemRecipePanels.Length; i++)
        {
            if (i > _recipes.Length - 1)
            {
                _itemRecipePanels[i].gameObject.SetActive(false);
            }
            else
            {
                _itemRecipePanels[i].gameObject.SetActive(true);
                _itemRecipePanels[i].Initialize(_recipes[i].Result.ItemData);
            }
        }
    }
}