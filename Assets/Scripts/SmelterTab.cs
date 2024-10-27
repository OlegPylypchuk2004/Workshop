using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SmelterTab : Tab
{
    [SerializeField] private SetItemSlot[] _setItemSlots;
    [SerializeField] private ResultItemSlot _resultItemSlot;
    [SerializeField] private Recipe[] _recipes;
    [SerializeField] private Button _startSmeltingButton;
    [SerializeField] private ItemSelectorTab _itemSelector;

    private Recipe _currentRecipe;
    private SetItemSlot _clickedSetItemSlot;

    public override void Open()
    {
        base.Open();

        foreach (SetItemSlot itemSlot in _setItemSlots)
        {
            itemSlot.Clicked += OnSetItemSlotClicked;
        }

        _startSmeltingButton.onClick.AddListener(OnStartSmeltingButtonClicked);

        if (_setItemSlots.Any(slot => slot.GetItemData() != null))
        {
            CheckCraftingAvailability();
        }
    }

    public override void Close()
    {
        base.Close();

        foreach (SetItemSlot itemSlot in _setItemSlots)
        {
            itemSlot.Clicked -= OnSetItemSlotClicked;
        }

        _startSmeltingButton.onClick.RemoveAllListeners();
    }

    private void OnSetItemSlotClicked(SetItemSlot setItemSlot)
    {
        if (setItemSlot.GetItemData() == null)
        {
            _clickedSetItemSlot = setItemSlot;

            _itemSelector.Open();
            _itemSelector.ItemSelected += OnItemSelected;

            Close();
        }
        else
        {
            setItemSlot.SetItem(null);
            CheckCraftingAvailability();
        }
    }

    private void OnItemSelected(ItemData data)
    {
        _itemSelector.ItemSelected -= OnItemSelected;

        _clickedSetItemSlot.SetItem(data);
        _clickedSetItemSlot = null;

        Open();
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
            if (IsCanCraft(recipe, slotItems))
            {
                _currentRecipe = recipe;
                _resultItemSlot.SetItem(recipe.Result.ItemData);
                return;
            }
        }

        _resultItemSlot.SetItem(null);
    }

    private bool IsCanCraft(Recipe recipe, List<ItemData> slotItems)
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

        foreach (var ingredient in _currentRecipe.Ingredients)
        {
            Storage.RemoveItem(ingredient.ItemData, ingredient.Quantity);
        }

        Storage.AddItem(_currentRecipe.Result.ItemData);

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