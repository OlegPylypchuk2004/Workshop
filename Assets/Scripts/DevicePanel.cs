using System;
using UnityEngine;
using UnityEngine.UI;

public class DevicePanel : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Tab _tab;

    public event Action<Tab> Clicked;

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
        Clicked?.Invoke(_tab);
    }
}