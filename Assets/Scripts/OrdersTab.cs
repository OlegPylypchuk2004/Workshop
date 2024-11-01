using System.Collections.Generic;
using UnityEngine;

public class OrdersTab : Tab
{
    [SerializeField] private OrderPanel _panelPrefab;
    [SerializeField] private RectTransform _panelsRectTransform;
    [SerializeField] private TopBar _topBar;
    [SerializeField] private OrdersManager _ordersManager;

    private List<OrderPanel> _panels = new List<OrderPanel>();

    private void Start()
    {
        foreach (Order order in _ordersManager.GetOrders())
        {
            SpawnOrderPanel(order);
        }

        _ordersManager.OrderCreated += SpawnOrderPanel;
    }

    private void OnDestroy()
    {
        _ordersManager.OrderCreated -= SpawnOrderPanel;
    }

    public override void Open()
    {
        base.Open();
        _topBar.SetTitleText("Orders");
    }

    private void SpawnOrderPanel(Order order)
    {
        OrderPanel panel = Instantiate(_panelPrefab, _panelsRectTransform);
        panel.Initialize(order);
        _panels.Add(panel);
        panel.OrderSubmitted += OnOrderChangedStatus;
        panel.OrderRejected += OnOrderChangedStatus;
    }

    private void OnOrderChangedStatus(OrderPanel panel)
    {
        panel.OrderSubmitted -= OnOrderChangedStatus;
        panel.OrderRejected -= OnOrderChangedStatus;
        _panels.Remove(panel);

        foreach (OrderPanel orderPanel in _panels)
        {
            orderPanel.UpdateView();
        }
    }
}