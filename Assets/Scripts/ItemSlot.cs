using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image _itemIconImage;
    [SerializeField] private TextMeshProUGUI _itemNameText;

    private ItemData _itemData;
    private int _itemQuantity;

    public event Action<ItemData> ItemChanged;

    public void SetItem(ItemData data, int quantity = 1)
    {
        if (data == null)
        {
            _itemIconImage.sprite = null;
            _itemNameText.text = "";
            _itemIconImage.gameObject.SetActive(false);
        }
        else
        {
            _itemIconImage.sprite = data.Icon;

            if (quantity == 1)
            {
                _itemNameText.text = $"{data.Name}";
            }
            else
            {
                _itemNameText.text = $"x{quantity} {data.Name}";
            }

            _itemIconImage.gameObject.SetActive(true);
        }

        _itemData = data;
        _itemQuantity = quantity;

        ItemChanged?.Invoke(data);
    }

    public ItemData GetItemData()
    {
        return _itemData;
    }

    public int GetItemQuantity()
    {
        return _itemQuantity;
    }
}