using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoragePanel : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _quantityText;
    [SerializeField] private TextMeshProUGUI _priceText;

    public void Initialize(ItemData itemData, int itemQuantity)
    {
        _iconImage.sprite = itemData.Icon;
        _nameText.text = itemData.Name;
        _quantityText.text = $"{itemQuantity}";
        _priceText.text = $"{itemData.Price}";
    }
}