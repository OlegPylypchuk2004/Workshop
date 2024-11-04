using UnityEngine;
using UnityEngine.UI;

public class EquipmentTab : Tab
{
    [SerializeField] private OpenEquipmentPanel[] _equipmentPanels;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Panel _shopPanel;
    [SerializeField] private TopBar _topBar;

    public override void Open()
    {
        base.Open();

        foreach (OpenEquipmentPanel equipmentPanel in _equipmentPanels)
        {
            equipmentPanel.Clicked += OnDevicePanelClicked;
        }

        _shopButton.onClick.AddListener(OnShopButtonClicked);

        _topBar.SetTitleText("Equipment");
    }

    public override void Close()
    {
        base.Close();

        foreach (OpenEquipmentPanel equipmentPanel in _equipmentPanels)
        {
            equipmentPanel.Clicked -= OnDevicePanelClicked;
        }

        _shopButton.onClick.RemoveAllListeners();
    }

    private void OnDevicePanelClicked(EquipmentPanel equipmentPanel)
    {
        NavigationController.Instance.OpenPanel(equipmentPanel);
    }

    private void OnShopButtonClicked()
    {
        NavigationController.Instance.OpenPanel(_shopPanel);
    }
}