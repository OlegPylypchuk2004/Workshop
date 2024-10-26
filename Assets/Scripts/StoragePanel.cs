using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoragePanel : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _quantityText;
    [SerializeField] private TextMeshProUGUI _priceText;

    public void Initialize(ItemData itemData)
    {
        _nameText.text = itemData.Name;
        _iconImage.sprite = itemData.Icon;
        _quantityText.text = $"{Random.Range(1, 10)}";
        _priceText.text = $"{Random.Range(1, 10)}";
    }
}