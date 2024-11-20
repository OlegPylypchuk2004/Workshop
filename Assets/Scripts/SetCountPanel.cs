using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetCountPanel : Panel
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _quantityText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private GameObject _priceView;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private TopBar _topBar;

    private int _minValue = 0;
    private int _maxValue = 1;

    private bool _isNeedPriceView;
    private int _pricePerItem;

    public event Action<int> CountChosen;
    public event Action CountChooseCanceled;

    public override void Open()
    {
        base.Open();

        _slider.onValueChanged.AddListener(OnSliderValueChanged);

        _cancelButton.onClick.AddListener(OnCancelButtonClicked);
        _confirmButton.onClick.AddListener(OnConfirmButtonClicked);

        _topBar.SetCreditsCountViewEnabled(true);
        _topBar.SetTitleText("Set the quantity");
    }

    public override void Close()
    {
        base.Close();

        _slider.onValueChanged.RemoveListener(OnSliderValueChanged);

        _cancelButton.onClick.RemoveAllListeners();
        _confirmButton.onClick.RemoveAllListeners();
    }

    private void OnSliderValueChanged(float value)
    {
        _slider.value = Mathf.RoundToInt(_slider.value);

        UpdateView();
    }

    private void OnConfirmButtonClicked()
    {
        CountChosen?.Invoke(GetValue());
    }

    private void OnCancelButtonClicked()
    {
        CountChooseCanceled?.Invoke();

        OnCloseButtonClicked();
    }

    public void Initialize(int minValue, int maxValue, bool isNeedPriceView = false, int pricePerItem = 0)
    {
        _minValue = minValue;
        _maxValue = maxValue;

        _slider.minValue = _minValue;
        _slider.maxValue = _maxValue;

        _slider.value = _minValue;

        _isNeedPriceView = isNeedPriceView;
        _pricePerItem = pricePerItem;
        _priceView.gameObject.SetActive(_isNeedPriceView);

        UpdateView();
    }

    public int GetValue()
    {
        int value = Mathf.RoundToInt(_slider.value);

        return value;
    }

    private void UpdateView()
    {
        _quantityText.text = $"x{_slider.value} / {_maxValue}";

        if (_isNeedPriceView)
        {
            _priceText.text = TextFormatter.FormatValue(_slider.value * _pricePerItem);
        }
    }
}