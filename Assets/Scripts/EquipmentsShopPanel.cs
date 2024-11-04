using System;
using UnityEngine;

public class EquipmentsShopPanel : Panel
{
    [SerializeField] private BuyEquipmentPanel _panelPrefab;
    [SerializeField] private RectTransform _panelsRectTransform;
    [SerializeField] private TopBar _topBar;
    [SerializeField] private NavigationBar _navigationBar;
    [SerializeField] private InGameNotificationsManager _inGameNotificationManager;

    private BuyEquipmentPanel[] _panels;

    private void Awake()
    {
        EquipmentData[] equipmentDatas = Resources.LoadAll<EquipmentData>("Equipments");
        _panels = new BuyEquipmentPanel[equipmentDatas.Length];

        for (int i = 0; i < equipmentDatas.Length; i++)
        {
            BuyEquipmentPanel panel = Instantiate(_panelPrefab, _panelsRectTransform);
            panel.Initialize(equipmentDatas[i]);
            _panels[i] = panel;
        }
    }

    public override void Open()
    {
        base.Open();

        _topBar.SetTitleText("Equipments shop");
        _topBar.SetCreditsCountViewEnabled(true);
        _navigationBar.gameObject.SetActive(false);

        foreach (BuyEquipmentPanel panel in _panels)
        {
            panel.EquipmentPurchased += OnEquipmentPurchased;
        }
    }

    public override void Close()
    {
        base.Close();

        _navigationBar.gameObject.SetActive(true);

        foreach (BuyEquipmentPanel panel in _panels)
        {
            panel.EquipmentPurchased -= OnEquipmentPurchased;
        }
    }

    private void OnEquipmentPurchased(EquipmentData data)
    {
        _inGameNotificationManager.ShowEquipmentPurchasedNotification(data);
    }
}