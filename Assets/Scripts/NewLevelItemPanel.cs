using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewLevelItemPanel : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;

    public void Initialize(Sprite sprite, string name)
    {
        _iconImage.sprite = sprite;
        _nameText.text = name;
    }
}