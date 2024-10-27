using System;
using UnityEngine;

public class NavigationBar : MonoBehaviour
{
    [SerializeField] private NavigationBarButton[] _buttons;
    [SerializeField] private Tab[] _tabs;
    [SerializeField] private Tab[] _allTabs;

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

        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].Clicked += OnButtonClicked;
            _buttons[i].UpdateView(i == _currentTabIndex);
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
        foreach (Tab tab in _allTabs)
        {
            tab.Close();
        }

        int buttonIndex = Array.IndexOf(_buttons, button);

        if (buttonIndex == _currentTabIndex)
        {
            return;
        }

        _tabs[_currentTabIndex].Close();
        _buttons[_currentTabIndex].UpdateView(false);
        _currentTabIndex = buttonIndex;
        _tabs[_currentTabIndex].Open();
        _buttons[_currentTabIndex].UpdateView(true);
    }
}