using System.Collections.Generic;
using UnityEngine;

public class OrdersTab : Tab
{
    [SerializeField] private OrderPanel _panelPrefab;
    [SerializeField] private RectTransform _panelsRectTransform;
    [SerializeField] private TopBar _topBar;

    private List<OrderPanel> _panels = new List<OrderPanel>();
    private ItemData[] _allItems;

    private void Start()
    {
        _allItems = Resources.LoadAll<ItemData>("Items");
        GenerateOrders();
    }

    private void GenerateOrders()
    {
        int ordersCount = Random.Range(5, 11);

        for (int i = 0; i < ordersCount; i++)
        {
            int orderResourcesCount = Random.Range(2, 6);
            OrderResource[] orderResources = new OrderResource[orderResourcesCount];

            for (int j = 0; j < orderResourcesCount; j++)
            {
                ItemData randomItemData = _allItems[Random.Range(0, _allItems.Length)];
                int resourcesCount = Random.Range(1, 26);
                orderResources[j] = new OrderResource(randomItemData, resourcesCount);
            }

            Order order = new Order(orderResources);

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