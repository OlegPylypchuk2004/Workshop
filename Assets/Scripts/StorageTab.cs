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

        int random = Random.Range(0, 10);

        for (int i = 0; i < random; i++)
        {
            StoragePanel panel = Instantiate(_panelPrefab, Vector3.zero, Quaternion.identity, _panelsRectTransform);
            panel.Initialize(Resources.Load<ItemData>("Items/Wood"));
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
}