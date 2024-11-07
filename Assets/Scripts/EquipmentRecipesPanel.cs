using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentRecipesPanel : MonoBehaviour
{
    [SerializeField] private Button _openButton;
    [SerializeField] private TextMeshProUGUI _nameText;

    private EquipmentData _equipmentData;

    public event Action<EquipmentData> Chosen;

    public void Initialize(EquipmentData equipmentData)
    {
        _equipmentData = equipmentData;

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