using System;
using UnityEngine;

public class NavigationBar : MonoBehaviour
{
    [SerializeField] private NavigationBarButton[] _buttons;
    [SerializeField] private Tab[] _tabs;

    private int _currentTabIndex;

    private void Awake()
    {
        for (int i = 0; i < _tabs.Length; i++)
        {
            if (i == _currentTabIndex)
            {
                _tabs[i].Open();
            }
            else
            {
                _tabs[i].Close();
            }
        }

        foreach (NavigationBarButton button in _buttons)
        {
            button.Clicked += OnButtonClicked;
        }
    }

    private void OnDestroy()
    {
        foreach (NavigationBarButton button in _buttons)
        {
            button.Clicked -= OnButtonClicked;
        }
    }

    private void OnButtonClicked(NavigationBarButton button)
    {
        int buttonIndex = Array.IndexOf(_buttons, button);

        if (buttonIndex == _currentTabIndex)
        {
            return;
        }

        _tabs[_currentTabIndex].Close();
        _currentTabIndex = buttonIndex;
        _tabs[_currentTabIndex].Open();
    }
}