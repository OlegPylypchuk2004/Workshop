using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenEquipmentPanel : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private EquipmentPanel _panel;
    [SerializeField] private TextMeshProUGUI _nameText;

    public event Action<EquipmentPanel> Clicked;

    private void Awake()
    {
        _button.onClick.AddListener(OnClicked);

        _nameText.text = _panel.GetItemData().Name;
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