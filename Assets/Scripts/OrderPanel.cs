using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private OrderResourcePanel _orderResourcePanelPrefab;
    [SerializeField] private RectTransform _orderResourcePanelRectTransform;
    [SerializeField] private TextMeshProUGUI _orderNameText;
    [SerializeField] private TextMeshProUGUI _creditsRewardText;
    [SerializeField] private TextMeshProUGUI _experiencePointsRewardText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Button _rejectButton;
    [SerializeField] private Button _submitButton;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private CanvasGroup _canvasGroup;

    private Order _order;
    private OrderResourcePanel[] _resourcePanels;

    public event Action<OrderPanel> OrderSubmitted;
    public event Action<OrderPanel> OrderRejected;

    public void Initialize(Order order)
    {
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, _rectTransform.sizeDelta.y + 125f * order.OrderResources.Length);

        _order = order;

        _resourcePanels = new OrderResourcePanel[order.OrderResources.Length];

        for (int i = 0; i < order.OrderResources.Length; i++)
        {
            OrderResourcePanel orderResourcePanel = Instantiate(_orderResourcePanelPrefab, Vector2.zero, Quaternion.identity, _orderResourcePanelRectTransform);
            orderResourcePanel.Initialize(order.OrderResources[i].ItemData, order.OrderResources[i].Quantity);
            _resourcePanels[i] = orderResourcePanel;
        }

        _orderNameText.text = order.CustomerName;
        _creditsRewardText.text = $"+{order.CreditsReward}";
        _experiencePointsRewardText.text = $"+{order.ExperiencePointsReward}";
        _timeText.text = TextFormatter.FormatTime(order.Time);

        _submitButton.interactable = IsCanSubmit();

        _order.CurrentTimeChanged += OnCurrentTimeChanged;
        _order.CurrentTimeIsUp += OnCurrentTimeIsUp;
    }

    private void OnEnable()
    {
        if (_order != null)
        {
            if (_order.IsTimeUp)
            {
                Destroy(gameObject);
                return;
            }

            _submitButton.interactable = IsCanSubmit();
            _order.CurrentTimeChanged += OnCurrentTimeChanged;
            _order.CurrentTimeIsUp += OnCurrentTimeIsUp;
        }

        if (_resourcePanels != null)
        {
            foreach (OrderResourcePanel resourcePanel in _resourcePanels)
            {
                resourcePanel.UpdateView();
            }
        }

        _rejectButton.onClick.AddListener(OnRejectButtonClicked);
        _submitButton.onClick.AddListener(OnSubmitButtonClicked);
    }

    private void OnDisable()
    {
        _rejectButton.onClick.RemoveAllListeners();
        _submitButton.onClick.RemoveAllListeners();

        _order.CurrentTimeChanged -= OnCurrentTimeChanged;
        _order.CurrentTimeIsUp -= OnCurrentTimeIsUp;
    }

    private void OnCurrentTimeChanged(float currentTime)
    {
        _timeText.text = TextFormatter.FormatTime(currentTime);
    }

    private void OnCurrentTimeIsUp()
    {
        Disappear();
    }

    private void OnRejectButtonClicked()
    {
        OrderRejected?.Invoke(this);

        Disappear();
    }

    private void OnSubmitButtonClicked()
    {
        if (!IsCanSubmit())
        {
            return;
        }

        foreach (OrderResourcePanel orderResourcePanel in _resourcePanels)
        {
            Storage.RemoveItem(orderResourcePanel.ItemData, orderResourcePanel.ItemQuantity);
        }

        PlayerDataManager.Data.CreditsCount += _order.CreditsReward;
        PlayerDataManager.Data.ExperiencePointsCount += _order.ExperiencePointsReward;

        OrderSubmitted?.Invoke(this);

        Disappear();
    }

    private bool IsCanSubmit()
    {
        foreach (OrderResourcePanel orderResourcePanel in _resourcePanels)
        {
            if (Storage.GetItemQuantity(orderResourcePanel.ItemData) < orderResourcePanel.ItemQuantity)
            {
                return false;
            }
        }

        return true;
    }

    private void Disappear()
    {
        Sequence disappearSequence = DOTween.Sequence();

        disappearSequence.AppendCallback(() =>
        {
            _canvasGroup.interactable = false;
        });

        disappearSequence.Append(_canvasGroup.DOFade(0f, 0.25f)
            .From(1f)
            .SetEase(Ease.InQuad));

        disappearSequence.AppendInterval(0.1f);

        disappearSequence.Append(_backgroundImage.DOFade(0f, 0.25f)
            .From(1f)
            .SetEase(Ease.InQuad));

        disappearSequence.Append(_rectTransform.DOSizeDelta(new Vector2(_rectTransform.sizeDelta.x, -50f), 0.25f)
            .SetEase(Ease.InQuad));

        disappearSequence.SetLink(gameObject);

        disappearSequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    public void UpdateView()
    {
        foreach (OrderResourcePanel resourcePanel in _resourcePanels)
        {
            resourcePanel.UpdateView();
        }

        _submitButton.interactable = IsCanSubmit();
    }
}