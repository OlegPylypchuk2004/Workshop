using UnityEngine;

public class RecipesTab : Tab
{
    [SerializeField] private EquipmentRecipesPanel[] _panels;
    [SerializeField] private TopBar _topBar;
    [SerializeField] private RecipesListPanel _recipesListPanel;

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