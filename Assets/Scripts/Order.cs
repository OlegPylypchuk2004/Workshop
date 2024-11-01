using System;
using System.Collections;
using UnityEngine;

public class Order
{
    private readonly string _customerName;
    private readonly OrderResource[] _orderResources;
    private readonly int _creditsReward;
    private readonly int _experiencePointsReward;
    private readonly float _time;

    public event Action<float> CurrentTimeChanged;
    public event Action<Order> Overdue;
    public event Action<Order> Submitted;
    public event Action<Order> Rejected;

    public Order(string customerName, int experiencePoints, OrderResource[] orderResources, float time)
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
        _time = time;

        CoroutineManager.Instance.StartCoroutine(LifeTimeCoroutine());
    }

    private IEnumerator LifeTimeCoroutine()
    {
        float currentTime = _time;

        while (currentTime > 0)
        {
            currentTime -= UnityEngine.Time.deltaTime;
            CurrentTimeChanged?.Invoke(currentTime);

            yield return null;
        }

        IsTimeUp = true;
        Overdue?.Invoke(this);
    }

    public void Submit()
    {
        Submitted?.Invoke(this);
    }

    public void Reject()
    {
        Rejected?.Invoke(this);
    }

    public string CustomerName => _customerName;
    public OrderResource[] OrderResources => _orderResources;
    public int CreditsReward => _creditsReward;
    public int ExperiencePointsReward => _experiencePointsReward;
    public float Time => _time;
    public bool IsTimeUp { get; private set; }
}