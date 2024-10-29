using System;
using UnityEngine;
using UnityEngine.UI;

public class SetItemSlot : ItemSlot
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _maskImage;
    [SerializeField] private GameObject _cross;

    public event Action<SetItemSlot> Clicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void OnClicked()
    {
        Clicked?.Invoke(this);
    }

    public void SetMaskEnabled(bool isEnabled)
    {
        _maskImage.gameObject.SetActive(isEnabled);
    }

    public void SetCrossEnabled(bool isEnabled)
    {
        _cross.SetActive(isEnabled);
    }
}