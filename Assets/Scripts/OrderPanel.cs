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
        _order.Overdue += OnOrderOverdue;
    }

    private void Awake()
    {
        _submitButton.interactable = false;
    }

    private void Start()
    {
        Appear();
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
            _order.Overdue += OnOrderOverdue;
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
        _order.Overdue -= OnOrderOverdue;
    }

    private void OnCurrentTimeChanged(float currentTime)
    {
        _timeText.text = TextFormatter.FormatTime(currentTime);
    }

    private void OnOrderOverdue(Order order)
    {
        Disappear();
    }

    private void OnRejectButtonClicked()
    {
        _order.Reject();
        Disappear();

        OrderRejected?.Invoke(this);
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

        _order.Submit();
        Disappear();

        OrderSubmitted?.Invoke(this);
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

    private void Appear()
    {
        Vector2 normalSize = _rectTransform.sizeDelta;
        _canvasGroup.interactable = false;

        Sequence appearSequence = DOTween.Sequence();

        appearSequence.Append(_rectTransform.DOSizeDelta(normalSize, 0.15f)
            .From(Vector2.zero)
            .SetEase(Ease.OutQuad));

        appearSequence.Append(_backgroundImage.DOFade(1f, 0.15f)
            .From(0f)
            .SetEase(Ease.OutQuad));

        appearSequence.AppendInterval(0.025f);

        appearSequence.Append(_canvasGroup.DOFade(1f, 0.15f)
            .From(0f)
            .SetEase(Ease.OutQuad));

        appearSequence.SetLink(gameObject);

        appearSequence.OnComplete(() =>
        {
            _canvasGroup.interactable = true;
        });
    }

    private void Disappear()
    {
        _canvasGroup.interactable = false;

        Sequence disappearSequence = DOTween.Sequence();

        disappearSequence.Append(_canvasGroup.DOFade(0f, 0.15f)
            .From(1f)
            .SetEase(Ease.InQuad));

        disappearSequence.AppendInterval(0.025f);

        disappearSequence.Append(_backgroundImage.DOFade(0f, 0.15f)
            .From(1f)
            .SetEase(Ease.InQuad));

        disappearSequence.Append(_rectTransform.DOSizeDelta(new Vector2(_rectTransform.sizeDelta.x, -50f), 0.15f)
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