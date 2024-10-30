using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SmelterPanel : Panel
{
    [SerializeField] private SetItemSlot[] _setItemSlots;
    [SerializeField] private ResultItemSlot _resultItemSlot;
    [SerializeField] private ItemSelectorPanel _itemSelector;
    [SerializeField] private TopBar _topBar;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private TextMeshProUGUI _smeltingTimerText;
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private Image _progressBarFiller;

    private Recipe[] _recipes;
    private Recipe _currentRecipe;
    private SetItemSlot _clickedSetItemSlot;
    private SmelterState _smelterState;
    private int _resultItemsQuantity;

    private void OnDestroy()
    {
        foreach (SetItemSlot setItemSlot in _setItemSlots)
        {
            if (setItemSlot.GetItemData() != null)
            {
                Storage.AddItem(setItemSlot.GetItemData(), setItemSlot.GetItemQuantity());
                setItemSlot.SetItem(null);
            }
        }
    }

    public override void Open()
    {
        base.Open();

        _recipes = Resources.LoadAll<Recipe>("Smelter/Recipes");

        foreach (SetItemSlot itemSlot in _setItemSlots)
        {
            itemSlot.Clicked += OnSetItemSlotClicked;
        }

        _button.onClick.AddListener(OnButtonClicked);

        if (_setItemSlots.Any(slot => slot.GetItemData() != null))
        {
            CheckCraftingAvailability();
        }

        _topBar.SetTitleText("Smelter");

        _itemSelector.ItemSelected -= OnItemSelected;

        UpdateState();
    }

    public override void Close()
    {
        base.Close();

        foreach (SetItemSlot itemSlot in _setItemSlots)
        {
            itemSlot.Clicked -= OnSetItemSlotClicked;
        }

        _button.onClick.RemoveAllListeners();
    }

    private void OnSetItemSlotClicked(SetItemSlot setItemSlot)
    {
        if (_smelterState != SmelterState.idle)
        {
            return;
        }

        if (setItemSlot.GetItemData() == null)
        {
            _clickedSetItemSlot = setItemSlot;

            NavigationController.Instance.OpenPanel(_itemSelector);

            _itemSelector.ItemSelected += OnItemSelected;
        }
        else
        {
            Storage.AddItem(setItemSlot.GetItemData(), setItemSlot.GetItemQuantity());
            setItemSlot.SetItem(null);
            CheckCraftingAvailability();
        }

        UpdateState();
    }

    private void OnItemSelected(ItemData data, int quantity)
    {
        NavigationController.Instance.ClosePanel();

        _itemSelector.ItemSelected -= OnItemSelected;

        Storage.RemoveItem(data, quantity);

        _clickedSetItemSlot.SetItem(data, quantity);
        _clickedSetItemSlot = null;

        CheckCraftingAvailability();
        UpdateState();
    }

    private void CheckCraftingAvailability()
    {
        _currentRecipe = null;
        _resultItemsQuantity = 0;

        foreach (Recipe recipe in _recipes)
        {
            if (IsCanCraft(recipe))
            {
                _currentRecipe = recipe;
                _resultItemsQuantity = CalculateResultQuantity(recipe);
                _resultItemSlot.SetItem(recipe.Result.ItemData, _resultItemsQuantity);
                return;
            }
        }

        _resultItemSlot.SetItem(null);
    }

    private int CalculateResultQuantity(Recipe recipe)
    {
        int maxQuantity = int.MaxValue;

        for (int i = 0; i < recipe.Ingredients.Length; i++)
        {
            if (recipe.Ingredients[i].ItemData == null)
                continue;

            int availableQuantity = 0;

            for (int j = 0; j < _setItemSlots.Length; j++)
            {
                if (_setItemSlots[j].GetItemData() == recipe.Ingredients[i].ItemData)
                {
                    availableQuantity = _setItemSlots[j].GetItemQuantity();
                    break;
                }
            }

            int possibleQuantity = availableQuantity / recipe.Ingredients[i].Quantity;
            maxQuantity = Mathf.Min(maxQuantity, possibleQuantity);
        }

        return maxQuantity > 0 ? maxQuantity : 0;
    }

    private bool IsCanCraft(Recipe recipe)
    {
        if (_setItemSlots.Length != recipe.Ingredients.Length)
        {
            return false;
        }

        bool[] matchedSlots = new bool[_setItemSlots.Length];

        for (int i = 0; i < recipe.Ingredients.Length; i++)
        {
            bool isMatched = false;
            for (int j = 0; j < _setItemSlots.Length; j++)
            {
                if (!matchedSlots[j] &&
                    _setItemSlots[j].GetItemData() == recipe.Ingredients[i].ItemData &&
                    _setItemSlots[j].GetItemQuantity() >= recipe.Ingredients[i].Quantity)
                {
                    matchedSlots[j] = true;
                    isMatched = true;
                    break;
                }
            }

            if (!isMatched)
                return false;
        }

        return true;
    }

    private void OnButtonClicked()
    {
        switch (_smelterState)
        {
            case SmelterState.idle:
                _smelterState = SmelterState.smelting;
                CoroutineManager.Instance.StartCoroutine(SmeltingCoroutine());
                break;

            case SmelterState.smelting:
                break;

            case SmelterState.done:
                Storage.AddItem(_currentRecipe.Result.ItemData, _resultItemsQuantity);

                foreach (SetItemSlot itemSlot in _setItemSlots)
                {
                    for (int i = 0; i < _currentRecipe.Ingredients.Length; i++)
                    {
                        if (itemSlot.GetItemData() == _currentRecipe.Ingredients[i].ItemData)
                        {
                            int spentQuantity = _currentRecipe.Ingredients[i].Quantity * _resultItemsQuantity;
                            itemSlot.SetItem(itemSlot.GetItemData(), itemSlot.GetItemQuantity() - spentQuantity);
                            break;
                        }
                    }
                }

                _resultItemSlot.SetItem(null);
                _currentRecipe = null;

                _smelterState = SmelterState.idle;
                break;
        }

        UpdateState();
    }

    private IEnumerator SmeltingCoroutine()
    {
        float smeltingTime = _currentRecipe.Time * _resultItemSlot.GetItemQuantity();
        float elapsedTime = 0f;

        _progressBar.SetActive(true);
        _smeltingTimerText.gameObject.SetActive(true);

        while (elapsedTime < smeltingTime)
        {
            elapsedTime += Time.deltaTime;
            float remainingTime = smeltingTime - elapsedTime;

            if (isActiveAndEnabled)
            {
                _smeltingTimerText.text = TextFormatter.FormatTime(remainingTime);
                _progressBarFiller.fillAmount = elapsedTime / smeltingTime;
            }

            yield return null;
        }

        _progressBar.SetActive(false);
        _smeltingTimerText.gameObject.SetActive(false);

        _smelterState = SmelterState.done;

        UpdateState();
    }

    private void UpdateState()
    {
        switch (_smelterState)
        {
            case SmelterState.idle:
                _button.interactable = _resultItemSlot.GetItemData() != null;
                _buttonText.text = "Start smelting";

                foreach (SetItemSlot itemSlot in _setItemSlots)
                {
                    itemSlot.SetMaskEnabled(false);
                    itemSlot.SetCrossEnabled(itemSlot.GetItemData() != null);
                }

                break;

            case SmelterState.smelting:
                _button.interactable = false;
                _buttonText.text = "Smelting...";

                foreach (SetItemSlot itemSlot in _setItemSlots)
                {
                    itemSlot.SetMaskEnabled(true);
                    itemSlot.SetCrossEnabled(false);
                }

                break;

            case SmelterState.done:
                _button.interactable = true;
                _buttonText.text = "Take";

                foreach (SetItemSlot itemSlot in _setItemSlots)
                {
                    if (itemSlot.GetItemQuantity() > _resultItemsQuantity)
                    {
                        Storage.AddItem(itemSlot.GetItemData(), itemSlot.GetItemQuantity() - _resultItemsQuantity);
                    }

                    itemSlot.SetMaskEnabled(true);
                    itemSlot.SetCrossEnabled(false);
                }

                break;
        }

        if (_currentRecipe == null || _smelterState != SmelterState.idle)
        {
            _resultItemSlot.SetTime();
        }
        else
        {
            _resultItemSlot.SetTime(_currentRecipe.Time);
        }
    }
}

public enum SmelterState
{
    idle,
    smelting,
    done
}
