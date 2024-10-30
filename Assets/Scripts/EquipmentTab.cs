using UnityEngine;

public class EquipmentTab : Tab
{
    [SerializeField] private EquipmentPanel[] _equipmentPanels;
    [SerializeField] private TopBar _topBar;

    public override void Open()
    {
        base.Open();

        foreach (EquipmentPanel equipmentPanel in _equipmentPanels)
        {
            equipmentPanel.Clicked += OnDevicePanelClicked;
        }

        _topBar.SetTitleText("Equipment");
    }

    public override void Close()
    {
        base.Close();

        foreach (EquipmentPanel equipmentPanel in _equipmentPanels)
        {
            equipmentPanel.Clicked -= OnDevicePanelClicked;
        }
    }

    private void OnDevicePanelClicked(Panel equipmentPanel)
    {
        NavigationController.Instance.OpenPanel(equipmentPanel);
    }
}