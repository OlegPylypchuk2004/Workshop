using DG.Tweening;
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
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _rectTransform;

    public MarketItem MarketItem { get; private set; }

    public event Action<MarketBuyItemPanel> BuyButtonClicked;

    private void Awake()
    {
        _buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    private void OnDestroy()
    {
        _buyButton.onClick.RemoveAllListeners();
    }

    public void Initialize(MarketItem marketItem)
    {
        MarketItem = marketItem;

        _iconImage.sprite = marketItem.ItemData.Icon;
        _nameText.text = marketItem.ItemData.Name;
        _quantityText.text = $"{marketItem.Quantity}";
        _priceText.text = $"{marketItem.Price}";
    }

    private void OnBuyButtonClicked()
    {
        BuyButtonClicked?.Invoke(this);
    }

    public void Disappear()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0f;

        Vector2 targetSize = new Vector2(_rectTransform.sizeDelta.x, -50f);

        _rectTransform.DOSizeDelta(targetSize, 0.25f)
            .SetEase(Ease.Linear)
            .SetLink(gameObject)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}