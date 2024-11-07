using UnityEngine;

public class RecipesTab : Tab
{
    [SerializeField] private EquipmentRecipesPanel _panelPrefab;
    [SerializeField] private RectTransform _panelsListRectTransform;
    [SerializeField] private TopBar _topBar;
    [SerializeField] private RecipesListPanel _recipesListPanel;

    private EquipmentRecipesPanel[] _panels;

    private void Awake()
    {
        EquipmentData[] equipmentDatas = Resources.LoadAll<EquipmentData>("Equipments");
        _panels = new EquipmentRecipesPanel[equipmentDatas.Length];

        for (int i = 0; i < equipmentDatas.Length; i++)
        {
            EquipmentRecipesPanel panel = Instantiate(_panelPrefab, _panelsListRectTransform);
            panel.Initialize(equipmentDatas[i]);
            _panels[i] = panel;
        }
    }

    public override void Open()
    {
        base.Open();

        foreach (EquipmentRecipesPanel panel in _panels)
        {
            panel.Chosen += OnPanelChosen;
        }

        _topBar.SetTitleText("Recipes");
    }

    public override void Close()
    {
        base.Close();

        foreach (EquipmentRecipesPanel panel in _panels)
        {
            panel.Chosen -= OnPanelChosen;
        }
    }

    private void OnPanelChosen(EquipmentData equipmentData)
    {
        NavigationController.Instance.OpenPanel(_recipesListPanel);
        _recipesListPanel.SetRecipes(Resources.LoadAll<Recipe>($"Recipes/{equipmentData.Name}"), equipmentData);
    }
}