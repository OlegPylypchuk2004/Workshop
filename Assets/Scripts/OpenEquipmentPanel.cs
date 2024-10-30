using System;
using UnityEngine;
using UnityEngine.UI;

public class OpenEquipmentPanel : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private EquipmentPanel _panel;

    public event Action<EquipmentPanel> Clicked;

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