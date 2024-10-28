using System;
using UnityEngine;
using UnityEngine.UI;

public class SetItemSlot : ItemSlot
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _maskImage;

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
}