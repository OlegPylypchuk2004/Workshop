using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _itemIconImage;
    [SerializeField] private TextMeshProUGUI _itemNameText;

    public event Action<ItemData> ItemChanged;

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
        SetItem(Resources.Load<ItemData>("Items/iron_ore"));
    }

    private void SetItem(ItemData itemData)
    {
        if (itemData == null)
        {
            _itemIconImage.sprite = null;
            _itemNameText.text = "empty";
            _itemIconImage.gameObject.SetActive(false);
        }
        else
        {
            _itemIconImage.sprite = itemData.Icon;
            _itemNameText.text = $"{itemData.Name}";
            _itemIconImage.gameObject.SetActive(true);
        }

        ItemChanged?.Invoke(itemData);
    }
}