using UnityEngine;

public class Order
{
    private readonly string _customerName;
    private readonly OrderResource[] _orderResources;
    private readonly int _creditsReward;

    public Order(string customerName, OrderResource[] orderResources)
    {
        _customerName = customerName;
        _orderResources = orderResources;

        int itemsPrice = 0;

        for (int i = 0; i < orderResources.Length; i++)
        {
            itemsPrice += orderResources[i].ItemData.Price * orderResources[i].Quantity;
        }

        _creditsReward = Mathf.RoundToInt(itemsPrice * Random.Range(1.1f, 1.25f));
    }

    public string CustomerName => _customerName;
    public OrderResource[] OrderResources => _orderResources;
    public int CreditsReward => _creditsReward;
}