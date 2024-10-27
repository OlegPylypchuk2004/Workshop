using UnityEngine;
using System.Linq;

public class Smelter : MonoBehaviour
{
    [SerializeField] private SetItemSlot[] _setItemSlots;
    [SerializeField] private ResultItemSlot _resultItemSlot;
    [SerializeField] private Recipe[] _recipes;

    private void OnEnable()
    {
        foreach (ItemSlot itemSlot in _setItemSlots)
        {
            itemSlot.ItemChanged += OnItemInSetSlotChanged;
        }
    }

    private void OnDisable()
    {
        foreach (ItemSlot itemSlot in _setItemSlots)
        {
            itemSlot.ItemChanged -= OnItemInSetSlotChanged;
        }
    }

    private void OnItemInSetSlotChanged(ItemData data)
    {
        CheckCraftingAvailability();
    }

    private void CheckCraftingAvailability()
    {
        ItemData[] slotItems = _setItemSlots.Select(slot => slot.GetItemData()).ToArray();

        foreach (Recipe recipe in _recipes)
        {
            if (CanCraft(recipe, slotItems))
            {
                _resultItemSlot.SetItem(recipe.Result.ItemData);
                return;
            }
        }

        _resultItemSlot.SetItem(null);
    }

    private bool CanCraft(Recipe recipe, ItemData[] slotItems)
    {
        foreach (RecipeIngredient ingredient in recipe.Ingredients)
        {
            int countInSlots = slotItems.Count(item => item == ingredient.ItemData);
            if (countInSlots < ingredient.Quantity)
            {
                return false;
            }
        }

        return true;
    }
}