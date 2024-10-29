using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderResourcePanel : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _quantityText;
    [SerializeField] private Color _greenColor;
    [SerializeField] private Color _redColor;

    private ItemData _itemData;
    private int _quantity;

    public void Initialize(ItemData itemData, int quantity)
    {
        _icon.sprite = itemData.Icon;
        _nameText.text = itemData.Name;

        _itemData = itemData;
        _quantity = quantity;

        UpdateView();
    }

    public void UpdateView()
    {
        int quantityInStorage = Storage.GetItemQuantity(_itemData);

        _quantityText.text = $"x{quantityInStorage}/{_quantity}";

        if (quantityInStorage >= _quantity)
        {
            _quantityText.color = _greenColor;
        }
        else
        {
            _quantityText.color = _redColor;
        }
    }
}