using TMPro;
using UnityEngine;

public class OrderPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private OrderResourcePanel _orderResourcePanelPrefab;
    [SerializeField] private RectTransform _orderResourcePanelRectTransform;
    [SerializeField] private TextMeshProUGUI _orderNameText;
    [SerializeField] private TextMeshProUGUI _creditsRewardText;

    private OrderResourcePanel[] _resourcePanels;

    public void Initialize(Order order)
    {
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, _rectTransform.sizeDelta.y + 125f * order.OrderResources.Length);

        _resourcePanels = new OrderResourcePanel[order.OrderResources.Length];

        for (int i = 0; i < order.OrderResources.Length; i++)
        {
            OrderResourcePanel orderResourcePanel = Instantiate(_orderResourcePanelPrefab, Vector2.zero, Quaternion.identity, _orderResourcePanelRectTransform);
            orderResourcePanel.Initialize(order.OrderResources[i].ItemData, order.OrderResources[i].Quantity);
            _resourcePanels[i] = orderResourcePanel;
        }

        _orderNameText.text = order.CustomerName;
        _creditsRewardText.text = $"+{order.CreditsReward}";
    }

    private void OnEnable()
    {
        if (_resourcePanels == null)
        {
            return;
        }

        foreach (OrderResourcePanel resourcePanel in _resourcePanels)
        {
            resourcePanel.UpdateView();
        }
    }
}