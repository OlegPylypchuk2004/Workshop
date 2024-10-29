using System.Collections.Generic;
using UnityEngine;

public class OrdersTab : Tab
{
    [SerializeField] private OrderPanel _panelPrefab;
    [SerializeField] private RectTransform _panelsRectTransform;
    [SerializeField] private TopBar _topBar;

    private List<OrderPanel> _panels = new List<OrderPanel>();
    private OrderData[] _orderDatas;

    private void Start()
    {
        _orderDatas = Resources.LoadAll<OrderData>("Orders");
        GenerateOrders();
    }

    private void GenerateOrders()
    {
        int ordersCount = Random.Range(5, 11);

        for (int i = 0; i < ordersCount; i++)
        {
            OrderData randomOrderData = _orderDatas[Random.Range(0, _orderDatas.Length)];

            List<ItemData> availableItems = new List<ItemData>(randomOrderData.Items);

            int orderResourcesCount = Random.Range(2, Mathf.Min(6, availableItems.Count + 1));
            OrderResource[] orderResources = new OrderResource[orderResourcesCount];

            for (int j = 0; j < orderResourcesCount; j++)
            {
                int randomIndex = Random.Range(0, availableItems.Count);
                ItemData randomItemData = availableItems[randomIndex];
                availableItems.RemoveAt(randomIndex);

                int resourcesCount = Random.Range(1, 26);
                orderResources[j] = new OrderResource(randomItemData, resourcesCount);
            }

            Order order = new Order(randomOrderData.CustomerName, orderResources);

            OrderPanel panel = Instantiate(_panelPrefab, _panelsRectTransform);
            panel.Initialize(order);
            _panels.Add(panel);
        }
    }

    public override void Open()
    {
        base.Open();
        _topBar.SetTitleText("Orders");
    }
}