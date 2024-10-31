using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentRecipesPanel : MonoBehaviour
{
    [SerializeField] private EquipmentData _equipmentData;
    [SerializeField] private Button _openButton;
    [SerializeField] private TextMeshProUGUI _nameText;

    public event Action<EquipmentData> Chosen;

    private void Awake()
    {
        _nameText.text = _equipmentData.Name;
    }

    private void OnEnable()
    {
        _openButton.onClick.AddListener(OnOpenButtonClicked);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveAllListeners();
    }

    private void OnOpenButtonClicked()
    {
        Chosen?.Invoke(_equipmentData);
    }
}