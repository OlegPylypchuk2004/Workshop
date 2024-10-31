using UnityEngine;

public class InGameNotificationsManager : MonoBehaviour
{
    [SerializeField] private InGameNotification _notificationPrefab;
    [SerializeField] private RectTransform _notificationsParent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            InGameNotification notification = Instantiate(_notificationPrefab);
            notification.transform.SetParent(_notificationsParent);
            notification.transform.SetSiblingIndex(0);
            notification.Initialize($"Notification");
        }
    }
}
