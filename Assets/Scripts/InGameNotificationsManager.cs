using UnityEngine;

public class InGameNotificationsManager : MonoBehaviour
{
    [SerializeField] private InGameNotification _notificationPrefab;
    [SerializeField] private RectTransform _notificationsParent;
    [SerializeField] private OrdersManager _ordersManager;
    [SerializeField] private Color _greenNotificationsColor;
    [SerializeField] private Color _redNotificationsColor;

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
        InGameNotification notification = Instantiate(_notificationPrefab, _notificationsParent);
        notification.transform.SetSiblingIndex(0);

        return notification;
    }

    private void OnItemAdded(ItemData itemData, int quantity)
    {
        ColoredTextData[] coloredTextDatas = new ColoredTextData[1];
        coloredTextDatas[0] = new ColoredTextData(itemData.Name.ToLower(), _greenNotificationsColor);

        GetNewNotification().Initialize($"+x{quantity} {itemData.Name.ToLower()}", coloredTexts: coloredTextDatas);
    }

    private void OnItemRemoved(ItemData itemData, int quantity)
    {
        ColoredTextData[] coloredTextDatas = new ColoredTextData[1];
        coloredTextDatas[0] = new ColoredTextData(itemData.Name.ToLower(), _redNotificationsColor);

        GetNewNotification().Initialize($"-x{quantity} {itemData.Name.ToLower()}", coloredTexts: coloredTextDatas);
    }

    private void OnCreditsCountIncreased(int result)
    {
        string formattedResult = TextFormatter.FormatValue(result);

        ColoredTextData[] coloredTextDatas = new ColoredTextData[1];
        coloredTextDatas[0] = new ColoredTextData($"+{formattedResult}", _greenNotificationsColor);

        GetNewNotification().Initialize($"+{formattedResult}", isShowCreditsIcon: true, coloredTexts: coloredTextDatas);
    }

    private void OnCreditsCountDecreased(int result)
    {
        string formattedResult = TextFormatter.FormatValue(result);

        ColoredTextData[] coloredTextDatas = new ColoredTextData[1];
        coloredTextDatas[0] = new ColoredTextData(formattedResult, _redNotificationsColor);

        GetNewNotification().Initialize($"{formattedResult}", isShowCreditsIcon: true, coloredTexts: coloredTextDatas);
    }

    private void OnExperiencePointsCountChanged(int count)
    {
        GetNewNotification().Initialize($"+{TextFormatter.FormatValue(count)}", isShowExperiencePointsIcon: true);
    }

    private void OnOrderCreated(Order order)
    {
        ColoredTextData[] coloredTextDatas = new ColoredTextData[1];
        coloredTextDatas[0] = new ColoredTextData("New", _greenNotificationsColor);

        GetNewNotification().Initialize($"New order", coloredTexts: coloredTextDatas);
    }

    private void OnOrderOverdue(Order order)
    {
        ColoredTextData[] coloredTextDatas = new ColoredTextData[1];
        coloredTextDatas[0] = new ColoredTextData("overdue", _redNotificationsColor);

        GetNewNotification().Initialize($"Order is overdue", coloredTexts: coloredTextDatas);
    }

    private void OnOrderSubmitted(Order order)
    {
        ColoredTextData[] coloredTextDatas = new ColoredTextData[1];
        coloredTextDatas[0] = new ColoredTextData("completed", _greenNotificationsColor);

        GetNewNotification().Initialize($"Order is completed", coloredTexts: coloredTextDatas);
    }

    private void OnOrderRejected(Order order)
    {
        ColoredTextData[] coloredTextDatas = new ColoredTextData[1];
        coloredTextDatas[0] = new ColoredTextData("rejected", _redNotificationsColor);

        GetNewNotification().Initialize($"Order is rejected", coloredTexts: coloredTextDatas);
    }

    public void ShowEquipmentPurchasedNotification(EquipmentData data)
    {
        ColoredTextData[] coloredTextDatas = new ColoredTextData[1];
        coloredTextDatas[0] = new ColoredTextData("purchased", _greenNotificationsColor);

        GetNewNotification().Initialize($"{data.Name} purchased", coloredTexts: coloredTextDatas);
    }
}