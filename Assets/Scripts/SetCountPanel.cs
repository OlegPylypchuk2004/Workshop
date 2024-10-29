using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetCountPanel : Panel
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _confirmButton;

    private int _minValue = 0;
    private int _maxValue = 1;

    public event Action<int> CountChosen;
    public event Action CountChooseCanceled;

    private void Awake()
    {
        Initialize(_minValue, _maxValue);
    }

    public override void Open()
    {
        base.Open();

        _slider.onValueChanged.AddListener(OnSliderValueChanged);

        _cancelButton.onClick.AddListener(OnCancelButtonClicked);
        _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
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

    public void Initialize(int minValue, int maxValue)
    {
        _minValue = minValue;
        _maxValue = maxValue;

        _slider.minValue = _minValue;
        _slider.maxValue = _maxValue;

        _slider.value = _minValue;

        UpdateView();
    }

    public int GetValue()
    {
        int value = Mathf.RoundToInt(_slider.value);

        return value;
    }

    private void UpdateView()
    {
        _text.text = $"x{_slider.value} / {_maxValue}";
    }
}