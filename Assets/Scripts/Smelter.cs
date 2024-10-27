using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Smelter : MonoBehaviour
{
    [SerializeField] private SetItemSlot[] _setItemSlots;
    [SerializeField] private ResultItemSlot _resultItemSlot;
    [SerializeField] private Recipe[] _recipes;
    [SerializeField] private Button _startSmeltingButton;

    private Recipe _currentRecipe;

    private void OnEnable()
    {
        foreach (ItemSlot itemSlot in _setItemSlots)
        {
            itemSlot.ItemChanged += OnItemInSetSlotChanged;
        }

        _startSmeltingButton.onClick.AddListener(OnStartSmeltingButtonClicked);
    }

    private void OnDisable()
    {
        foreach (ItemSlot itemSlot in _setItemSlots)
        {
            itemSlot.ItemChanged -= OnItemInSetSlotChanged;
        }

        _startSmeltingButton.onClick.RemoveAllListeners();
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

        _currentRecipe = null;

        foreach (var recipe in _recipes)
        {
            if (CanCraft(recipe, slotItems))
            {
                _currentRecipe = recipe;
                _resultItemSlot.SetItem(recipe.Result.ItemData);
                return;
            }
        }

        _resultItemSlot.SetItem(null);
    }

    private bool CanCraft(Recipe recipe, List<ItemData> slotItems)
    {
        var itemsToMatch = new List<ItemData>(slotItems);

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

    private void OnStartSmeltingButtonClicked()
    {
        if (_currentRecipe == null || !AreIngredientsAvailable(_currentRecipe))
        {
            Debug.Log("Not enough ingredients in storage.");
            return;
        }

        // Видаляємо інгредієнти зі Storage
        foreach (var ingredient in _currentRecipe.Ingredients)
        {
            Storage.RemoveItem(ingredient.ItemData, ingredient.Quantity);
        }

        // Додаємо результат до Storage
        Storage.AddItem(_currentRecipe.Result.ItemData);

        Debug.Log($"Crafted: {_currentRecipe.Result.ItemData.Name}");

        // Очищаємо слоти після крафту
        foreach (var slot in _setItemSlots)
        {
            slot.SetItem(null);
        }

        _resultItemSlot.SetItem(null);
    }

    private bool AreIngredientsAvailable(Recipe recipe)
    {
        foreach (var ingredient in recipe.Ingredients)
        {
            int availableQuantity = Storage.GetItemQuantity(ingredient.ItemData);
            if (availableQuantity < ingredient.Quantity)
            {
                return false;
            }
        }
        return true;
    }
}
