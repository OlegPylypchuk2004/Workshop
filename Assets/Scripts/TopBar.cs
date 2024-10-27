using TMPro;
using UnityEngine;

public class TopBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void SetTitleText(string text)
    {
        _titleText.text = text;
    }
}