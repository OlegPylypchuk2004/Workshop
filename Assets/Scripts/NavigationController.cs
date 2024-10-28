using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    [SerializeField] private NavigationBar _navigationBar;
    [SerializeField] private TopBar _topBar;
    [SerializeField] private bool _isDebug;

    private Stack<INavigationElement> navigationStack = new Stack<INavigationElement>();

    public static NavigationController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_isDebug)
        {
            Debug.LogError(GetNavigationStackContents());
        }
    }

    public void OpenTab(INavigationElement tab)
    {
        if (navigationStack.Count > 0 && navigationStack.Peek() != tab)
        {
            navigationStack.Peek().Close();
            navigationStack.Clear();
        }

        tab.Open();
        navigationStack.Push(tab);

        _navigationBar.gameObject.SetActive(true);
        _topBar.SetBackButtonEnabled(false);
    }

    public void OpenPanel(INavigationElement panel)
    {
        if (navigationStack.Count > 0)
            navigationStack.Peek().Close();

        panel.Open();
        navigationStack.Push(panel);

        _navigationBar.gameObject.SetActive(false);
        _topBar.SetBackButtonEnabled(true);
    }

    public void ClosePanel()
    {
        if (navigationStack.Count == 0) return;

        navigationStack.Pop().Close();

        if (navigationStack.Count > 0)
            navigationStack.Peek().Open();

        UpdateNavigationBarAndTopBar();
    }

    private void UpdateNavigationBarAndTopBar()
    {
        bool isTabOnTop = navigationStack.Count > 0 && navigationStack.Peek() is Tab;

        _navigationBar.gameObject.SetActive(isTabOnTop);
        _topBar.SetBackButtonEnabled(!isTabOnTop);
    }

    public string GetNavigationStackContents()
    {
        StringBuilder stackContents = new StringBuilder("Current Stack: ");
        foreach (var item in navigationStack)
        {
            stackContents.Append(item.ToString()).Append(" -> ");
        }

        if (navigationStack.Count > 0)
            stackContents.Length -= 4;

        return stackContents.ToString();
    }
}