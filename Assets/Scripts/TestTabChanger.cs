using UnityEngine;

public class TestTabChanger : MonoBehaviour
{
    [SerializeField] private StorageTab _storageTab;
    [SerializeField] private SmelterPanel _smelterTab;

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
    }

    private void CloseAllButtons()
    {
        _storageTab.Close();
        _smelterTab.Close();
    }
}