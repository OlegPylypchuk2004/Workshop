using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemRecipePanel : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Button _viewButton;

    public ItemData ItemData { get; private set; }

    public void Initialize(ItemData itemData)
    {
        ItemData = itemData;

        _iconImage.sprite = itemData.Icon;
        _nameText.text = itemData.Name;
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

    }
}