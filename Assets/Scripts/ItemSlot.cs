using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image _itemIconImage;
    [SerializeField] private TextMeshProUGUI _itemNameText;

    private ItemData _itemData;

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
            _itemNameText.text = $"{data.Name}";
            _itemIconImage.gameObject.SetActive(true);
        }

        _itemData = data;

        ItemChanged?.Invoke(data);
    }

    public ItemData GetItemData()
    {
        return _itemData;
    }
}