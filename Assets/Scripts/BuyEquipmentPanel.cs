using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyEquipmentPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TextMeshProUGUI _buyButtonText;

    private EquipmentData _equipmentData;

    public event Action<EquipmentData> EquipmentPurchased;

    public void Initialize(EquipmentData equipmentData)
    {
        _equipmentData = equipmentData;

        _nameText.text = _equipmentData.Name;
        _descriptionText.text = _equipmentData.Description;
        _priceText.text = TextFormatter.FormatValue(equipmentData.Price);

        if (PlayerDataManager.Data.IsPurchasedEquipment(_equipmentData))
        {
            _buyButton.interactable = false;
            _buyButtonText.text = "Purchased";
        }
        else
        {
            _buyButton.interactable = true;
            _buyButtonText.text = "Buy";
        }
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveAllListeners();
    }

    private void OnBuyButtonClicked()
    {
        if (_equipmentData == null || PlayerDataManager.Data.IsPurchasedEquipment(_equipmentData))
        {
            return;
        }

        if (PlayerDataManager.Data.CreditsCount >= _equipmentData.Price)
        {
            PlayerDataManager.Data.CreditsCount -= _equipmentData.Price;
            PlayerDataManager.Data.AddEquipmentToPurchasingEquipmentsList(_equipmentData);

            _buyButton.interactable = false;
            _buyButtonText.text = "Purchased";

            EquipmentPurchased?.Invoke(_equipmentData);
        }
    }
}