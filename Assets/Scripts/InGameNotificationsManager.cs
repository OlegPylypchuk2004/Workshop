using UnityEngine;

public class InGameNotificationsManager : MonoBehaviour
{
    [SerializeField] private InGameNotification _notificationPrefab;
    [SerializeField] private RectTransform _notificationsParent;

    private void OnEnable()
    {
        Storage.ItemAdded += OnItemAdded;
        Storage.ItemRemoved += OnItemRemoved;
    }

    private void OnDisable()
    {
        Storage.ItemAdded -= OnItemAdded;
        Storage.ItemRemoved -= OnItemRemoved;
    }

    private void OnItemAdded(ItemData itemData, int quantity)
    {
        GetNewNotification().Initialize($"+x{quantity} {itemData.Name.ToLower()}");
    }

    private void OnItemRemoved(ItemData itemData, int quantity)
    {
        GetNewNotification().Initialize($"-x{quantity} {itemData.Name.ToLower()}");
    }

    private InGameNotification GetNewNotification()
    {
        InGameNotification notification = Instantiate(_notificationPrefab);
        notification.transform.SetParent(_notificationsParent);
        notification.transform.SetSiblingIndex(0);

        return notification;
    }
}