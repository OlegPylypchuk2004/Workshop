using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    private Stack<INavigationElement> navigationStack = new Stack<INavigationElement>();
    public int NavigationStackCount { get; private set; } // ���� ��� ��������� ������� �������� � �����

    public NavigationBar _navigationBar;
    public TopBar _topBar;

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
        Debug.LogError(GetNavigationStackContents());
    }

    public void OpenTab(INavigationElement tab)
    {
        if (navigationStack.Count > 0 && navigationStack.Peek() != tab)
        {
            navigationStack.Peek().Close();
            navigationStack.Clear(); // ������� ���� ��� ������� �� ���� �������
        }

        tab.Open();
        navigationStack.Push(tab);
        UpdateStackCount();

        _navigationBar.gameObject.SetActive(true);
        _topBar.SetBackButtonEnabled(false);
    }

    public void OpenPanel(INavigationElement panel)
    {
        if (navigationStack.Count > 0)
            navigationStack.Peek().Close(); // ������� ��������� �����

        panel.Open(); // ³������ ����� �����
        navigationStack.Push(panel); // ������ �� �����
        UpdateStackCount();

        _navigationBar.gameObject.SetActive(false);
        _topBar.SetBackButtonEnabled(true);
    }

    public void ClosePanel()
    {
        if (navigationStack.Count == 0) return;

        navigationStack.Pop().Close(); // ������� �������� �����

        if (navigationStack.Count > 0)
            navigationStack.Peek().Open(); // �������� ��������� �����

        UpdateStackCount();

        _navigationBar.gameObject.SetActive(false);

        if (navigationStack.Count > 0 && navigationStack.Peek() is Tab)
        {
            _navigationBar.gameObject.SetActive(true);
            _topBar.SetBackButtonEnabled(false);
        }
        else
        {
            _navigationBar.gameObject.SetActive(false);
            _topBar.SetBackButtonEnabled(true);
        }
    }

    //public void OpenLast()
    //{
    //    if (navigationStack.Count > 0)
    //    {
    //        INavigationElement lastPanelOrTab = navigationStack.Peek();
    //        lastPanelOrTab.Open();
    //    }
    //}

    private void UpdateStackCount()
    {
        NavigationStackCount = navigationStack.Count;
    }

    public string GetNavigationStackContents()
    {
        StringBuilder stackContents = new StringBuilder("Current Stack: ");
        foreach (var item in navigationStack)
        {
            stackContents.Append(item.ToString()).Append(" -> ");
        }
        if (navigationStack.Count > 0)
            stackContents.Length -= 4; // ��������� ������� ������

        return stackContents.ToString();
    }
}