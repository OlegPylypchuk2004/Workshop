using UnityEngine;
using UnityEngine.UI;

public class SetItemSlot : ItemSlot
{
    [SerializeField] private Button _button;
    [SerializeField] private ItemSelectorTab _itemSelector;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void OnClicked()
    {
        _itemSelector.Open();
        _itemSelector.ItemSelected += OnItemSelected;
    }

    private void OnItemSelected(ItemData data)
    {
        _itemSelector.ItemSelected -= OnItemSelected;
        SetItem(data);
    }
}