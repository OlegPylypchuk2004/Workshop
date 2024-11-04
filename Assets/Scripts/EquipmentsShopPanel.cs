using UnityEngine;

public class EquipmentsShopPanel : Panel
{
    [SerializeField] private BuyEquipmentPanel _panelPrefab;
    [SerializeField] private RectTransform _panelsRectTransform;
    [SerializeField] private TopBar _topBar;
    [SerializeField] private NavigationBar _navigationBar;

    private void Awake()
    {
        EquipmentData[] equipmentDatas = Resources.LoadAll<EquipmentData>("Equipments");

        for (int i = 0; i < equipmentDatas.Length; i++)
        {
            BuyEquipmentPanel panel = Instantiate(_panelPrefab, _panelsRectTransform);
            panel.Initialize(equipmentDatas[i]);
        }
    }

    public override void Open()
    {
        base.Open();

        _topBar.SetTitleText("Equipments shop");
        _navigationBar.gameObject.SetActive(false);
    }

    public override void Close()
    {
        base.Close();

        _navigationBar.gameObject.SetActive(true);
    }
}