using UnityEngine;
using UnityEngine.UI;

public class RecipePanel : Panel
{
    [SerializeField] private TopBar _topBar;
    [SerializeField] private ItemSlot _resultItemSlot;
    [SerializeField] private ItemSlot[] _ingredientItemSlots;
    [SerializeField] private Image _linesImage;
    [SerializeField] private Sprite[] _lineSprites;

    public void SetRecipe(Recipe recipe)
    {
        base.Open();

        _topBar.SetTitleText($"{recipe.Result.ItemData.Name} recipe");
        _resultItemSlot.SetItem(recipe.Result.ItemData, recipe.Result.Quantity);

        int resultItemsCount = 0;

        for (int i = 0; i < _ingredientItemSlots.Length; i++)
        {
            if (i > recipe.Ingredients.Length - 1)
            {
                _ingredientItemSlots[i].gameObject.SetActive(false);
            }
            else
            {
                if (recipe.Ingredients[i].ItemData == null)
                {
                    _ingredientItemSlots[i].gameObject.SetActive(false);
                }
                else
                {
                    _ingredientItemSlots[i].gameObject.SetActive(true);
                    _ingredientItemSlots[i].SetItem(recipe.Ingredients[i].ItemData, recipe.Ingredients[i].Quantity);

                    resultItemsCount++;
                }
            }
        }

        _linesImage.sprite = _lineSprites[resultItemsCount - 1];
        _linesImage.SetNativeSize();
    }
}