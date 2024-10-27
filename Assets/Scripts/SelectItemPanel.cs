using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemPanel : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _quantityText;
    [SerializeField] private Button _selectButton;

    private ItemData _itemData;

    public event Action<SelectItemPanel> SelectButtonClicked;

    private void OnEnable()
    {
        _selectButton.onClick.AddListener(OnSelectButtonClicked);
    }

    private void OnDisable()
    {
        _selectButton.onClick.RemoveAllListeners();
    }

    private void OnSelectButtonClicked()
    {
        SelectButtonClicked?.Invoke(this);
    }

    public void Initialize(ItemData itemData, int itemQuantity)
    {
        _itemData = itemData;

        _iconImage.sprite = itemData.Icon;
        _nameText.text = itemData.Name;
        _quantityText.text = $"{itemQuantity}";
    }

    public ItemData ItemData
    {
        get
        {
            return _itemData;
        }
    }
}