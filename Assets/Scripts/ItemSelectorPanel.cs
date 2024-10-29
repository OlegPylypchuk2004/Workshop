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
    [SerializeField] private SetCountPanel _setCountPanel;

    private SelectItemPanel _selectItemPanel;

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

        _emptyStorageText.gameObject.SetActive(_panels.Count == 0);
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
        _selectItemPanel = panel;
        _setCountPanel.Initialize(1, Storage.GetItemQuantity(panel.ItemData));
        _setCountPanel.CountChosen += OnCountChosen;

        NavigationController.Instance.OpenPanel(_setCountPanel);
    }

    private void OnCountChosen(int count)
    {
        _selectItemPanel = null;
        _setCountPanel.CountChosen -= OnCountChosen;

        NavigationController.Instance.ClosePanel();

        ItemSelected?.Invoke(_selectItemPanel.ItemData);

        Debug.Log(count);
    }
}