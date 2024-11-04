using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersManager : MonoBehaviour
{
    private List<Order> _orders = new List<Order>();
    private OrderData[] _orderDatas;
    private int _plannedOrdersCount;
    private int _maxOrdersCount;
    private float _baseOrdersIncreaseDelay;
    private float _ordersDelayCoef;
    private float _orderResourcesCountCoef;

    public event Action<Order> OrderCreated;
    public event Action<Order> OrderOverdue;
    public event Action<Order> OrderSubmitted;
    public event Action<Order> OrderRejected;

    private void Awake()
    {
        _orderDatas = Resources.LoadAll<OrderData>("Orders");

        GameRules gameRules = Resources.Load<GameRules>("GameRules");
        _maxOrdersCount = gameRules.MaxOrdersCount;
        _baseOrdersIncreaseDelay = gameRules.BaseOrdersIncreaseDelay;
        _ordersDelayCoef = gameRules.OrdersDelayCoef;
        _orderResourcesCountCoef = gameRules.OrderResourcesCountCoef;
    }

    private void Update()
    {
        int totalOrdersCount = _orders.Count + _plannedOrdersCount;

        if (totalOrdersCount < _maxOrdersCount)
        {
            if (_plannedOrdersCount == 0)
            {
                StartCoroutine(ToPlanOrder());
            }
        }
    }

    private IEnumerator ToPlanOrder()
    {
        _plannedOrdersCount++;
        float delay = _baseOrdersIncreaseDelay + _orders.Count * _ordersDelayCoef;
        yield return new WaitForSeconds(delay);

        _plannedOrdersCount--;
        CreateOrder();
    }

    private void CreateOrder()
    {
        OrderData orderData = _orderDatas[UnityEngine.Random.Range(0, _orderDatas.Length)];
        List<ItemData> availableItems = new List<ItemData>(orderData.Items);
        OrderResource[] orderResources = new OrderResource[orderData.Items.Length];

        for (int j = 0; j < orderResources.Length; j++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableItems.Count);
            ItemData randomItemData = availableItems[randomIndex];
            availableItems.RemoveAt(randomIndex);

            int resourcesCount = Mathf.RoundToInt(UnityEngine.Random.Range(1, 6) * _orderResourcesCountCoef + LevelManager.GetCurrentLevel() / 5);
            orderResources[j] = new OrderResource(randomItemData, resourcesCount);
        }

        int experiencePoints = Mathf.RoundToInt(orderData.ExperiencePointsPerItem * orderResources.Length);
        Order order = new Order(orderData.CustomerName, experiencePoints, orderResources, orderData.TimeIsSeconds);
        _orders.Add(order);

        order.Overdue += OnOrderOverdue;
        order.Submitted += OnOrderSubmitted;
        order.Rejected += OnOrderRejected;

        OrderCreated?.Invoke(order);
    }

    private void OnOrderOverdue(Order order)
    {
        order.Overdue -= OnOrderOverdue;
        order.Submitted -= OnOrderSubmitted;
        order.Rejected -= OnOrderRejected;

        _orders.Remove(order);

        OrderOverdue?.Invoke(order);
    }

    private void OnOrderSubmitted(Order order)
    {
        order.Overdue -= OnOrderOverdue;
        order.Submitted -= OnOrderSubmitted;
        order.Rejected -= OnOrderRejected;

        _orders.Remove(order);

        OrderSubmitted?.Invoke(order);
    }

    private void OnOrderRejected(Order order)
    {
        order.Overdue -= OnOrderOverdue;
        order.Submitted -= OnOrderSubmitted;
        order.Rejected -= OnOrderRejected;

        _orders.Remove(order);

        OrderRejected?.Invoke(order);
    }

    public Order[] GetOrders()
    {
        return _orders.ToArray();
    }
}