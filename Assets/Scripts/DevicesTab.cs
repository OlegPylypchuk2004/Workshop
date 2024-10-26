using UnityEngine;
using UnityEngine.UI;

public class DevicesTab : Tab
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private GameObject _deviceOptionsPanel;
    [SerializeField] private DevicePanel[] _devicePanels;

    private void Awake()
    {
        foreach (DevicePanel devicePanel in _devicePanels)
        {
            devicePanel.Clicked += OnDevicePanelClicked;
        }
    }

    private void OnDestroy()
    {
        foreach (DevicePanel devicePanel in _devicePanels)
        {
            devicePanel.Clicked -= OnDevicePanelClicked;
        }
    }

    public override void Close()
    {
        base.Close();

        _scrollRect.gameObject.SetActive(true);
        _deviceOptionsPanel.gameObject.SetActive(false);
    }

    private void OnDevicePanelClicked(DevicePanel devicePanel)
    {
        _scrollRect.gameObject.SetActive(false);
        _deviceOptionsPanel.gameObject.SetActive(true);
    }
}