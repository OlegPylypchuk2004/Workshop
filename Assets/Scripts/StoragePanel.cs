using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoragePanel : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _quantityText;
    [SerializeField] private TextMeshProUGUI _priceText;

    private void Awake()
    {
        Initialize(_itemData);
    }

    public void Initialize(ItemData itemData)
    {
        _nameText.text = itemData.Name;
        _iconImage.sprite = itemData.Icon;
        _descriptionText.text = itemData.Description;
        _quantityText.text = $"{Random.Range(1, 10)}";
        _priceText.text = $"{Random.Range(1, 10)}";
    }
}