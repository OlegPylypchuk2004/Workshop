using System.Collections.Generic;
using UnityEngine;

public class StorageTab : Tab
{
    [SerializeField] private StoragePanel _panelPrefab;
    [SerializeField] private RectTransform _panelsRectTransform;

    private List<StoragePanel> _panels;

    private void Awake()
    {
        _panels = new List<StoragePanel>();
    }

    public override void Open()
    {
        base.Open();

        RemoveAllPanels();

        foreach (var item in Storage.GetAllItems())
        {
            StoragePanel panel = Instantiate(_panelPrefab, Vector3.zero, Quaternion.identity, _panelsRectTransform);
            panel.Initialize(item.Key, item.Value);
            _panels.Add(panel);
        }
    }

    public override void Close()
    {
        base.Close();

        RemoveAllPanels();
    }

    private void RemoveAllPanels()
    {
        if (_panels.Count > 0)
        {
            foreach (StoragePanel panel in _panels)
            {
                Destroy(panel.gameObject);
            }

            _panels.Clear();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Storage.AddItem(Resources.Load<ItemData>("Items/Wood"), Random.Range(1, 5));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Storage.AddItem(Resources.Load<ItemData>("Items/Steel"), Random.Range(1, 5));
        }
    }
}