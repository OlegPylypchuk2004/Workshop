using System;
using UnityEngine;

public class NavigationBar : MonoBehaviour
{
    [SerializeField] private NavigationBarButton[] _navigationButtons;
    [SerializeField] private Tab[] _tabs;

    private int _currentTabIndex;

    private void Awake()
    {
        for (int i = 0; i < _navigationButtons.Length; i++)
        {
            _navigationButtons[i].Clicked += OnNavigationButtonClicked;
            _navigationButtons[i].UpdateView(i == _currentTabIndex);
        }

        NavigationController.Instance.OpenTab(_tabs[_currentTabIndex]);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _navigationButtons.Length; i++)
        {
            _navigationButtons[i].Clicked -= OnNavigationButtonClicked;
        }
    }

    private void OnNavigationButtonClicked(NavigationBarButton navigationButton)
    {
        _navigationButtons[_currentTabIndex].UpdateView(false);
        NavigationController.Instance.ClosePanel();

        _currentTabIndex = Array.IndexOf(_navigationButtons, navigationButton);

        _navigationButtons[_currentTabIndex].UpdateView(true);
        NavigationController.Instance.OpenTab(_tabs[_currentTabIndex]);
    }
}