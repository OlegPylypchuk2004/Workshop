using System;
using UnityEngine;
using UnityEngine.UI;

public class NavigationBarButton : MonoBehaviour
{
    [SerializeField] private Button _button;

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
}