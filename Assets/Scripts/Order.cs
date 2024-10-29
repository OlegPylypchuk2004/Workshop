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

        float orderRewardInCreditsCoef = Resources.Load<GameRules>("GameRules").OrderRewardInCreditsCoef;
        _creditsReward = Mathf.RoundToInt(itemsPrice * orderRewardInCreditsCoef);
    }

    public string CustomerName => _customerName;
    public OrderResource[] OrderResources => _orderResources;
    public int CreditsReward => _creditsReward;
}