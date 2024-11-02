using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPanel : Panel
{
    [SerializeField] private EquipmentData _data;
    [SerializeField] private SetItemSlot[] _setItemSlots;
    [SerializeField] private ResultItemSlot _resultItemSlot;
    [SerializeField] private ItemSelectorPanel _itemSelector;
    [SerializeField] private TopBar _topBar;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Slider _slider;

    private Recipe[] _recipes;
    private Recipe _currentRecipe;
    private SetItemSlot _clickedSetItemSlot;
    private EquipmentState _state;
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

        _recipes = Resources.LoadAll<Recipe>($"Recipes/{_data.Name}");

        foreach (SetItemSlot itemSlot in _setItemSlots)
        {
            itemSlot.Clicked += OnSetItemSlotClicked;
        }

        _button.onClick.AddListener(OnButtonClicked);

        if (_setItemSlots.Any(slot => slot.GetItemData() != null))
        {
            CheckCraftingAvailability();
        }

        _topBar.SetTitleText(_data.Name);

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
        if (_state != EquipmentState.Idle)
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
            maxQuantity = Mathf.Min(maxQuantity, possibleQuantity) * recipe.Result.Quantity;
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
        switch (_state)
        {
            case EquipmentState.Idle:
                _state = EquipmentState.AtWork;
                CoroutineManager.Instance.StartCoroutine(WorkCoroutine());
                break;

            case EquipmentState.AtWork:
                break;

            case EquipmentState.Done:
                Storage.AddItem(_currentRecipe.Result.ItemData, _resultItemsQuantity);

                foreach (SetItemSlot itemSlot in _setItemSlots)
                {
                    for (int i = 0; i < _currentRecipe.Ingredients.Length; i++)
                    {
                        if (itemSlot.GetItemData() == _currentRecipe.Ingredients[i].ItemData)
                        {
                            int spentQuantity = _currentRecipe.Ingredients[i].Quantity * _resultItemsQuantity;
                            int quantity = itemSlot.GetItemQuantity() - spentQuantity;

                            if (quantity <= 0)
                            {
                                itemSlot.SetItem(null);
                            }
                            else
                            {
                                itemSlot.SetItem(itemSlot.GetItemData(), quantity);
                            }

                            break;
                        }
                    }
                }

                _resultItemSlot.SetItem(null);
                _currentRecipe = null;

                _state = EquipmentState.Idle;
                break;
        }

        UpdateState();
    }

    private IEnumerator WorkCoroutine()
    {
        float workTime = _currentRecipe.Time * _resultItemSlot.GetItemQuantity();
        float elapsedTime = 0f;

        _slider.gameObject.SetActive(true);
        _timerText.gameObject.SetActive(true);

        while (elapsedTime < workTime)
        {
            elapsedTime += Time.deltaTime;
            float remainingTime = workTime - elapsedTime;

            if (isActiveAndEnabled)
            {
                _timerText.text = TextFormatter.FormatTime(remainingTime);
                _slider.value = elapsedTime / workTime;
            }

            yield return null;
        }

        _slider.gameObject.SetActive(false);
        _timerText.gameObject.SetActive(false);

        _state = EquipmentState.Done;

        UpdateState();
    }

    private void UpdateState()
    {
        switch (_state)
        {
            case EquipmentState.Idle:
                _button.interactable = _resultItemSlot.GetItemData() != null;
                _buttonText.text = _data.IdleStateButtonText;

                foreach (SetItemSlot itemSlot in _setItemSlots)
                {
                    itemSlot.SetMaskEnabled(false);
                    itemSlot.SetCrossEnabled(itemSlot.GetItemData() != null);
                }
                break;

            case EquipmentState.AtWork:
                _button.interactable = false;
                _buttonText.text = _data.AtWorkStateButtonText;

                foreach (SetItemSlot itemSlot in _setItemSlots)
                {
                    itemSlot.SetMaskEnabled(true);
                    itemSlot.SetCrossEnabled(false);
                }
                break;

            case EquipmentState.Done:
                _button.interactable = true;
                _buttonText.text = _data.DoneStateButtonText;

                foreach (SetItemSlot itemSlot in _setItemSlots)
                {
                    itemSlot.SetMaskEnabled(true);
                    itemSlot.SetCrossEnabled(false);
                }
                break;
        }

        if (_currentRecipe == null || _state != EquipmentState.Idle)
        {
            _resultItemSlot.SetTime();
        }
        else
        {
            _resultItemSlot.SetTime(_currentRecipe.Time);
        }
    }

    public EquipmentData GetItemData()
    {
        return _data;
    }
}

public enum EquipmentState
{
    Idle,
    AtWork,
    Done
}