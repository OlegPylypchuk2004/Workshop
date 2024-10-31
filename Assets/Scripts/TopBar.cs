using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private Button _backButton;
    [SerializeField] private LevelView _levelView;
    [SerializeField] private CreditsCountView _creditsCountView;

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

    public void SetBackButtonEnabled(bool isEnabled)
    {
        _backButton.gameObject.SetActive(isEnabled);
    }

    public void SetLevelViewEnabled(bool isEnabled)
    {
        _levelView.gameObject.SetActive(isEnabled);
    }

    public void SetCreditsCountViewEnabled(bool isEnabled)
    {
        _creditsCountView.gameObject.SetActive(isEnabled);
    }
}