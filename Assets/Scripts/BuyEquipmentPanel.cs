using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyEquipmentPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Button _buyButton;

    private EquipmentData _equipmentData;

    public void Initialize(EquipmentData equipmentData)
    {
        _equipmentData = equipmentData;

        _nameText.text = _equipmentData.Name;
        _descriptionText.text = _equipmentData.Description;
        _priceText.text = TextFormatter.FormatValue(equipmentData.Price);
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
        if (PlayerDataManager.Data.CreditsCount >= _equipmentData.Price)
        {

        }
    }
}