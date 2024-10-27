using UnityEngine;

public class DevicesTab : Tab
{
    [SerializeField] private DevicePanel[] _devicePanels;
    [SerializeField] private TopBar _topBar;

    public override void Open()
    {
        base.Open();

        foreach (DevicePanel devicePanel in _devicePanels)
        {
            devicePanel.Clicked += OnDevicePanelClicked;
        }

        _topBar.SetTitleText("Select equipment");
    }

    public override void Close()
    {
        base.Close();

        foreach (DevicePanel devicePanel in _devicePanels)
        {
            devicePanel.Clicked -= OnDevicePanelClicked;
        }
    }

    private void OnDevicePanelClicked(Tab deviceTab)
    {
        Close();
        deviceTab.Open();
    }
}