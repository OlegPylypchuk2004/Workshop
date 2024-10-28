using System;
using UnityEngine;

public class NavigationBar : MonoBehaviour
{
    [SerializeField] private NavigationBarButton[] _navigationButtons;
    [SerializeField] private Tab[] _tabs;

    private int _currentTabIndex;

    private void Start()
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
        int newTabIndex = Array.IndexOf(_navigationButtons, navigationButton);

        if (newTabIndex != -1 && newTabIndex != _currentTabIndex)
        {
            _navigationButtons[_currentTabIndex].UpdateView(false); // Сховати попередню вкладку
            _currentTabIndex = newTabIndex; // Оновити індекс вкладки
            _navigationButtons[_currentTabIndex].UpdateView(true); // Відкрити нову вкладку

            NavigationController.Instance.OpenTab(_tabs[_currentTabIndex]);
        }
    }
}