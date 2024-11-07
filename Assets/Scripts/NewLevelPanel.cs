using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewLevelPanel : Panel
{
    [SerializeField] private TopBar _topBar;
    [SerializeField] private NewLevelItemPanel _itemPanelPrefab;
    [SerializeField] private RectTransform _itemsRectTransform;
    [SerializeField] private TextMeshProUGUI _newItemsText;
    [SerializeField] private TextMeshProUGUI _newRecipesText;
    [SerializeField] private TextMeshProUGUI _newEquipmentsText;
    [SerializeField] private Button _continueButton;

    private List<NewLevelItemPanel> _panels = new List<NewLevelItemPanel>();

    public override void Open()
    {
        base.Open();

        UpdateView();

        _continueButton.onClick.AddListener(OnContinueButtonClicked);
    }

    public override void Close()
    {
        base.Close();

        _continueButton.onClick.RemoveAllListeners();
    }

    private void OnContinueButtonClicked()
    {
        OnCloseButtonClicked();
    }

    private void UpdateView()
    {
        foreach (NewLevelItemPanel panel in _panels)
        {
            Destroy(panel.gameObject);
        }

        _panels.Clear();

        int level = LevelManager.GetCurrentLevel();

        _topBar.SetTitleText($"Level {level} unlocked");

        LevelData newLevelData = Resources.Load<LevelData>($"Levels/{level}");

        if (newLevelData != null)
        {
            if (newLevelData.NewItems.Length > 0)
            {
                _newItemsText.gameObject.SetActive(true);

                for (int i = 0; i < newLevelData.NewItems.Length; i++)
                {
                    NewLevelItemPanel newItemPanel = Instantiate(_itemPanelPrefab, _itemsRectTransform);
                    newItemPanel.Initialize(newLevelData.NewItems[i].Icon, newLevelData.NewItems[i].Name);
                    newItemPanel.transform.SetSiblingIndex(_newItemsText.transform.GetSiblingIndex() + i + 1);
                    _panels.Add(newItemPanel);
                }
            }
            else
            {
                _newItemsText.gameObject.SetActive(false);
            }

            if (newLevelData.NewRecipes.Length > 0)
            {
                _newRecipesText.gameObject.SetActive(true);

                for (int i = 0; i < newLevelData.NewRecipes.Length; i++)
                {
                    NewLevelItemPanel newItemPanel = Instantiate(_itemPanelPrefab, _itemsRectTransform);
                    newItemPanel.Initialize(newLevelData.NewRecipes[i].Result.ItemData.Icon, newLevelData.NewRecipes[i].Result.ItemData.Name);
                    newItemPanel.transform.SetSiblingIndex(_newRecipesText.transform.GetSiblingIndex() + i + 1);
                    _panels.Add(newItemPanel);
                }
            }
            else
            {
                _newRecipesText.gameObject.SetActive(false);
            }

            if (newLevelData.NewRecipes.Length > 0)
            {
                _newEquipmentsText.gameObject.SetActive(true);

                for (int i = 0; i < newLevelData.NewEquipments.Length; i++)
                {
                    NewLevelItemPanel newItemPanel = Instantiate(_itemPanelPrefab, _itemsRectTransform);
                    newItemPanel.Initialize(newLevelData.NewEquipments[i].Icon, newLevelData.NewEquipments[i].Name);
                    newItemPanel.transform.SetSiblingIndex(_newEquipmentsText.transform.GetSiblingIndex() + i + 1);
                    _panels.Add(newItemPanel);
                }
            }
            else
            {
                _newEquipmentsText.gameObject.SetActive(false);
            }
        }
    }
}