using UnityEngine;

public class RecipesTab : Tab
{
    [SerializeField] private EquipmentRecipesPanel[] _panels;
    [SerializeField] private TopBar _topBar;
    [SerializeField] private RecipesListPanel _recipesListPanel;

    public Recipe[] Recipes { get; private set; }
    public EquipmentData Equipment { get; private set; }

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
        Recipes = Resources.LoadAll<Recipe>($"Recipes/{equipmentData.Name}");
        Equipment = equipmentData;

        NavigationController.Instance.OpenPanel(_recipesListPanel);
    }
}