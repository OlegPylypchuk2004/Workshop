using UnityEngine;

public class RecipesListPanel : Panel
{
    [SerializeField] private RecipesTab _recipesTab;
    [SerializeField] private ItemRecipePanel[] _itemRecipePanels;
    [SerializeField] private RecipePanel _recipePanel;
    [SerializeField] private TopBar _topBar;

    private EquipmentData _equipmentData;

    public override void Open()
    {
        base.Open();

        for (int i = 0; i < _itemRecipePanels.Length; i++)
        {
            _itemRecipePanels[i].RecipeChosen += OnRecipeChosen;
        }

        if (_equipmentData != null)
        {
            _topBar.SetTitleText($"{_equipmentData.Name} recipes");
        }
    }

    public override void Close()
    {
        base.Close();

        for (int i = 0; i < _itemRecipePanels.Length; i++)
        {
            _itemRecipePanels[i].RecipeChosen -= OnRecipeChosen;
        }
    }

    public void SetRecipes(Recipe[] recipes, EquipmentData equipmentData)
    {
        for (int i = 0; i < _itemRecipePanels.Length; i++)
        {
            if (i > recipes.Length - 1)
            {
                _itemRecipePanels[i].gameObject.SetActive(false);
            }
            else
            {
                _itemRecipePanels[i].gameObject.SetActive(true);
                _itemRecipePanels[i].Initialize(recipes[i]);
            }
        }

        _equipmentData = equipmentData;
        _topBar.SetTitleText($"{_equipmentData.Name} recipes");
    }

    private void OnRecipeChosen(Recipe recipe)
    {
        NavigationController.Instance.OpenPanel(_recipePanel);
        _recipePanel.SetRecipe(recipe);
    }
}