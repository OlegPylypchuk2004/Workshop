using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour, INavigationElement
{
    [SerializeField] private Button _closeButton;

    public virtual void Open()
    {
        if (isActiveAndEnabled)
        {
            return;
        }

        gameObject.SetActive(true);

        _closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    public virtual void Close()
    {
        if (!isActiveAndEnabled)
        {
            return;
        }

        gameObject.SetActive(false);

        _closeButton.onClick.RemoveAllListeners();
    }

    protected void OnCloseButtonClicked()
    {
        NavigationController.Instance.ClosePanel();
    }
}