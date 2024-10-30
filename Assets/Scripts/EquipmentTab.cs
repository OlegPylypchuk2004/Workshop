using UnityEngine;

public class EquipmentTab : Tab
{
    [SerializeField] private OpenEquipmentPanel[] _equipmentPanels;
    [SerializeField] private TopBar _topBar;

    public override void Open()
    {
        base.Open();

        foreach (OpenEquipmentPanel equipmentPanel in _equipmentPanels)
        {
            equipmentPanel.Clicked += OnDevicePanelClicked;
        }

        _topBar.SetTitleText("Equipment");
    }

    public override void Close()
    {
        base.Close();

        foreach (OpenEquipmentPanel equipmentPanel in _equipmentPanels)
        {
            equipmentPanel.Clicked -= OnDevicePanelClicked;
        }
    }

    private void OnDevicePanelClicked(EquipmentPanel equipmentPanel)
    {
        NavigationController.Instance.OpenPanel(equipmentPanel);
    }
}