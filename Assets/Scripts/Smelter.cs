using System.Linq;
using UnityEngine;

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
        var slotItems = _setItemSlots
            .Select(slot => slot.GetItemData())
            .Where(item => item != null)
            .ToList();

        foreach (var recipe in _recipes)
        {
            if (CanCraft(recipe, slotItems))
            {
                _resultItemSlot.SetItem(recipe.Result.ItemData);
                return;
            }
        }

        _resultItemSlot.SetItem(null);
    }

    private bool CanCraft(Recipe recipe, System.Collections.Generic.List<ItemData> slotItems)
    {
        var itemsToMatch = new System.Collections.Generic.List<ItemData>(slotItems);

        foreach (var ingredient in recipe.Ingredients)
        {
            for (int i = 0; i < ingredient.Quantity; i++)
            {
                if (itemsToMatch.Contains(ingredient.ItemData))
                {
                    itemsToMatch.Remove(ingredient.ItemData);
                }
                else
                {
                    return false;
                }
            }
        }

        return itemsToMatch.Count == 0;
    }
}