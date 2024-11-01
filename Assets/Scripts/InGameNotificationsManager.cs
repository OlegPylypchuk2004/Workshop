using UnityEngine;

public class InGameNotificationsManager : MonoBehaviour
{
    [SerializeField] private InGameNotification _notificationPrefab;
    [SerializeField] private RectTransform _notificationsParent;
    [SerializeField] private OrdersManager _ordersManager;

    private void OnEnable()
    {
        Storage.ItemAdded += OnItemAdded;
        Storage.ItemRemoved += OnItemRemoved;

        PlayerDataManager.Data.CreditsCountIncreased += OnCreditsCountIncreased;
        PlayerDataManager.Data.CreditsCountDecreased += OnCreditsCountDecreased;
        PlayerDataManager.Data.ExperiencePointsChanged += OnExperiencePointsCountChanged;

        _ordersManager.OrderCreated += OnOrderCreated;
        _ordersManager.OrderOverdue += OnOrderOverdue;
        _ordersManager.OrderSubmitted += OnOrderSubmitted;
        _ordersManager.OrderRejected += OnOrderRejected;
    }

    private void OnDisable()
    {
        Storage.ItemAdded -= OnItemAdded;
        Storage.ItemRemoved -= OnItemRemoved;
        PlayerDataManager.Data.CreditsCountIncreased -= OnCreditsCountIncreased;
        PlayerDataManager.Data.CreditsCountDecreased -= OnCreditsCountDecreased;
        PlayerDataManager.Data.ExperiencePointsChanged -= OnExperiencePointsCountChanged;

        _ordersManager.OrderCreated -= OnOrderCreated;
        _ordersManager.OrderOverdue -= OnOrderOverdue;
        _ordersManager.OrderSubmitted -= OnOrderSubmitted;
        _ordersManager.OrderRejected -= OnOrderRejected;
    }

    private InGameNotification GetNewNotification()
    {
        InGameNotification notification = Instantiate(_notificationPrefab);
        notification.transform.SetParent(_notificationsParent);
        notification.transform.SetSiblingIndex(0);

        return notification;
    }

    private void OnItemAdded(ItemData itemData, int quantity)
    {
        GetNewNotification().Initialize($"+x{quantity} {itemData.Name.ToLower()}");
    }

    private void OnItemRemoved(ItemData itemData, int quantity)
    {
        GetNewNotification().Initialize($"-x{quantity} {itemData.Name.ToLower()}");
    }

    private void OnCreditsCountIncreased(int result)
    {
        GetNewNotification().Initialize($"+{result}", isShowCreditsIcon: true);
    }

    private void OnCreditsCountDecreased(int result)
    {
        GetNewNotification().Initialize($"{result}", isShowCreditsIcon: true);
    }

    private void OnExperiencePointsCountChanged(int count)
    {
        GetNewNotification().Initialize($"+{count}", isShowExperiencePointsIcon: true);
    }

    private void OnOrderCreated(Order order)
    {
        GetNewNotification().Initialize($"New order");
    }

    private void OnOrderOverdue(Order order)
    {
        GetNewNotification().Initialize($"Order is overdue");
    }

    private void OnOrderSubmitted(Order order)
    {
        GetNewNotification().Initialize($"Order is completed");
    }

    private void OnOrderRejected(Order order)
    {
        GetNewNotification().Initialize($"Order is rejected");
    }
}