using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenEquipmentPanel : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private EquipmentPanel _panel;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private EquipmentData _equipmentData;
    [SerializeField] private Slider _progressBarSlider;

    public event Action<EquipmentPanel> Clicked;

    private void Awake()
    {
        _button.onClick.AddListener(OnClicked);

        _nameText.text = _panel.GetItemData().Name;
    }

    private void Start()
    {
        _panel.WorkStarted += OnEquipmentWorkStarted;
        _panel.WorkCompleted += OnEquipmentWorkCompleted;
        _panel.WorkTimeChanged += OnEquipmentWorkTimeChanged;
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();

        _panel.WorkStarted -= OnEquipmentWorkStarted;
        _panel.WorkCompleted -= OnEquipmentWorkCompleted;
        _panel.WorkTimeChanged -= OnEquipmentWorkTimeChanged;
    }

    private void OnClicked()
    {
        Clicked?.Invoke(_panel);
    }

    private void OnEquipmentWorkStarted()
    {
        _progressBarSlider.gameObject.SetActive(true);
    }

    private void OnEquipmentWorkCompleted()
    {
        _progressBarSlider.gameObject.SetActive(false);
    }

    private void OnEquipmentWorkTimeChanged(float currentTime, float targetTime)
    {
        _progressBarSlider.value = currentTime / targetTime;
    }

    public  EquipmentData EquipmentData => _equipmentData;
}