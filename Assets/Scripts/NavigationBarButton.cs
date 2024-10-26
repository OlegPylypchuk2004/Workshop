using System;
using UnityEngine;
using UnityEngine.UI;

public class NavigationBarButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Color _defaultIconColor;
    [SerializeField] private Color _currentIconColor;

    public event Action<NavigationBarButton> Clicked;

    private void Awake()
    {
        _button.onClick.AddListener(OnClicked);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void OnClicked()
    {
        Clicked?.Invoke(this);
    }

    public void UpdateView(bool isCurrent)
    {
        if (isCurrent)
        {
            _iconImage.color = _currentIconColor;
        }
        else
        {
            _iconImage.color = _defaultIconColor;
        }
    }
}