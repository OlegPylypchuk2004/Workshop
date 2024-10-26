using System;
using UnityEngine;
using UnityEngine.UI;

public class DevicePanel : MonoBehaviour
{
    [SerializeField] private Button _button;

    public event Action<DevicePanel> Clicked;

    private void Awake()
    {
        _button.onClick.AddListener(OnClicked);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void OnClicked()
    {
        Clicked?.Invoke(this);
    }
}