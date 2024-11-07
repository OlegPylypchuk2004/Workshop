using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StorageTab : Tab
{
    [SerializeField] private StoragePanel _panelPrefab;
    [SerializeField] private RectTransform _panelsRectTransform;
    [SerializeField] private TopBar _topBar;
    [SerializeField] private TextMeshProUGUI _emptyStorageText;

    private List<StoragePanel> _panels = new List<StoragePanel>();

    public override void Open()
    {
        base.Open();

        RemoveAllPanels();

        foreach (var item in Storage.GetAllItems())
        {
            StoragePanel panel = Instantiate(_panelPrefab, _panelsRectTransform);
            panel.transform.SetSiblingIndex(0);
            panel.Initialize(item.Key, item.Value);
            _panels.Add(panel);
        }

        _topBar.SetTitleText("Storage");

        _emptyStorageText.gameObject.SetActive(_panels.Count == 0);
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
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PlayerDataManager.Data.ExperiencePointsCount += Random.Range(25, 50);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerDataManager.ClearAllData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
#endif
    }
}