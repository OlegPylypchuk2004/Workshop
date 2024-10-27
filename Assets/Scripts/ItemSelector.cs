using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelector : MonoBehaviour
{
    [SerializeField] private SelectItemPanel _panelPrefab;
    [SerializeField] private RectTransform _panelsRectTransform;
    [SerializeField] private TopBar _topBar;

    private List<SelectItemPanel> _panels = new List<SelectItemPanel>();

    public event Action<ItemData> ItemSelected;

    public void Open()
    {
        gameObject.SetActive(true);

        foreach (var item in Storage.GetAllItems())
        {
            SelectItemPanel panel = Instantiate(_panelPrefab, Vector3.zero, Quaternion.identity, _panelsRectTransform);
            panel.Initialize(item.Key, item.Value);
            panel.SelectButtonClicked += OnItemSelected;
            _panels.Add(panel);
        }

        _topBar.SetTitleText("Select item");
    }

    public void Close()
    {
        gameObject.SetActive(false);

        RemoveAllPanels();
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
        Close();
    }
}