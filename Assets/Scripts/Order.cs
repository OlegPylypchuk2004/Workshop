using UnityEngine;

public class Order
{
    private readonly string _customerName;
    private readonly OrderResource[] _orderResources;
    private readonly int _creditsReward;
    private readonly int _experiencePointsReward;

    public Order(string customerName, int experiencePoints, OrderResource[] orderResources)
    {
        _customerName = customerName;
        _orderResources = orderResources;

        int itemsPrice = 0;

        for (int i = 0; i < orderResources.Length; i++)
        {
            itemsPrice += orderResources[i].ItemData.Price * orderResources[i].Quantity;
        }

        GameRules gameRules = Resources.Load<GameRules>("GameRules");

        _creditsReward = Mathf.RoundToInt(itemsPrice * gameRules.OrderRewardInCreditsCoef);
        _experiencePointsReward = Mathf.RoundToInt(experiencePoints * gameRules.OrderRewardInExperiencePointsCoef);
    }

    public string CustomerName => _customerName;
    public OrderResource[] OrderResources => _orderResources;
    public int CreditsReward => _creditsReward;
    public int ExperiencePointsReward => _experiencePointsReward;
}