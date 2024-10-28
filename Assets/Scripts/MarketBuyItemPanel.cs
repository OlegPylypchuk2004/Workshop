using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketBuyItemPanel : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _quantityText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Button _buyButton;

    private ItemData _itemData;
    private int _quantity;
    private int _price;

    public event Action<MarketBuyItemPanel> BuyButtonClicked;

    private void Awake()
    {
        _buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    private void OnDestroy()
    {
        _buyButton.onClick.RemoveAllListeners();
    }

    public void Initialize(ItemData itemData, int itemQuantity, float priceCoef)
    {
        _quantity = itemQuantity;
        _price = Mathf.RoundToInt(itemData.Price * itemQuantity * priceCoef);

        _iconImage.sprite = itemData.Icon;
        _nameText.text = itemData.Name;
        _quantityText.text = $"{_quantity}";
        _priceText.text = $"{_price}";
    }

    private void OnBuyButtonClicked()
    {
        BuyButtonClicked?.Invoke(this);
    }

    public ItemData ItemData
    {
        get
        {
            return _itemData;
        }
    }
}