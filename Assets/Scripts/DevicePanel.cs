using System;
using UnityEngine;
using UnityEngine.UI;

public class DevicePanel : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Panel _panel;

    public event Action<Panel> Clicked;

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
        Clicked?.Invoke(_panel);
    }
}