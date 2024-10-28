using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSelectorPanel : Panel
{
    [SerializeField] private SelectItemPanel _panelPrefab;
    [SerializeField] private RectTransform _panelsRectTransform;
    [SerializeField] private TopBar _topBar;
    [SerializeField] private NavigationBar _navigationBar;
    [SerializeField] private TextMeshProUGUI _emptyStorageText;

    private List<SelectItemPanel> _panels = new List<SelectItemPanel>();

    public event Action<ItemData> ItemSelected;

    public override void Open()
    {
        base.Open();

        foreach (var item in Storage.GetAllItems())
        {
            SelectItemPanel panel = Instantiate(_panelPrefab, Vector3.zero, Quaternion.identity, _panelsRectTransform);
            panel.Initialize(item.Key, item.Value);
            panel.SelectButtonClicked += OnItemSelected;
            _panels.Add(panel);
        }

        _topBar.SetTitleText("Select item");
        _navigationBar.gameObject.SetActive(false);

        if (_panels.Count == 0)
        {
            _emptyStorageText.gameObject.SetActive(true);
        }
    }

    public override void Close()
    {
        base.Close();

        RemoveAllPanels();

        _navigationBar.gameObject.SetActive(true);
    }

    private void RemoveAllPanels()
    {
        if (_panels.Count > 0)
        {
            foreach (SelectItemPanel panel in _panels)
            {
                panel.SelectButtonClicked -= OnItemSelected;
                Destroy(panel.gameObject);
            }

            _panels.Clear();
        }
    }

    private void OnItemSelected(SelectItemPanel panel)
    {
        ItemSelected?.Invoke(panel.ItemData);

        NavigationController.Instance.ClosePanel();
        //NavigationController.Instance.OpenLast();
    }
}