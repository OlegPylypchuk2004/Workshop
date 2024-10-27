using UnityEngine;

public class TestTabChanger : MonoBehaviour
{
    [SerializeField] private StorageTab _storageTab;
    [SerializeField] private SmelterTab _smelterTab;
    [SerializeField] private ItemSelectorTab _itemSelectorTab;

    private void Start()
    {
        CloseAllButtons();
        _storageTab.Open();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            CloseAllButtons();
            _storageTab.Open();
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            CloseAllButtons();
            _smelterTab.Open();
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            CloseAllButtons();
            _itemSelectorTab.Open();
        }
    }

    private void CloseAllButtons()
    {
        _storageTab.Close();
        _smelterTab.Close();
        _itemSelectorTab.Close();
    }
}