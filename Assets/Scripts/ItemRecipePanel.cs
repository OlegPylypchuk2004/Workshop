using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemRecipePanel : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Button _viewButton;

    private Recipe _recipe;

    public event Action<Recipe> RecipeChosen;

    public void Initialize(Recipe recipe)
    {
        _recipe = recipe;

        _iconImage.sprite = recipe.Result.ItemData.Icon;
        _nameText.text = recipe.Result.ItemData.Name;
    }

    private void OnEnable()
    {
        _viewButton.onClick.AddListener(OnViewButtonClicked);
    }

    private void OnDisable()
    {
        _viewButton.onClick.RemoveAllListeners();
    }

    private void OnViewButtonClicked()
    {
        RecipeChosen?.Invoke(_recipe);
    }
}