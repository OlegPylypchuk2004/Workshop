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

    public event Action<ItemData, int> ItemSelected;

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
        int itemQuantity = Storage.GetItemQuantity(panel.ItemData);

        if (itemQuantity > 1)
        {
            _setCountPanel.Initialize(1, itemQuantity);
            _setCountPanel.CountChosen += OnCountChosen;
            _setCountPanel.CountChooseCanceled += OnCountChooseCanceled;

            NavigationController.Instance.OpenPanel(_setCountPanel);
        }
        else
        {
            ItemSelected?.Invoke(panel.ItemData, itemQuantity);
        }
    }

    private void OnCountChosen(int count)
    {
        _setCountPanel.CountChosen -= OnCountChosen;
        _setCountPanel.CountChooseCanceled -= OnCountChooseCanceled;

        NavigationController.Instance.ClosePanel();

        ItemSelected?.Invoke(_selectItemPanel.ItemData, count);

        _selectItemPanel = null;
    }

    private void OnCountChooseCanceled()
    {
        _setCountPanel.CountChosen -= OnCountChosen;
        _setCountPanel.CountChooseCanceled -= OnCountChooseCanceled;
    }
}