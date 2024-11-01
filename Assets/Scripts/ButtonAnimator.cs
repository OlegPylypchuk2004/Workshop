using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnimator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnValidate()
    {
        _button ??= GetComponentInChildren<Button>();
        _backgroundImage ??= GetComponent<Image>();
        _text ??= GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (_button.interactable)
        {
            _backgroundImage.color = Color.white;
            _text.color = Color.white;
        }
        else
        {
            Color targetColor = Color.white * 0.75f;
            targetColor.a = 1f;

            _backgroundImage.color = targetColor;
            _text.color = targetColor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            FadeOut();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FadeIn();
    }

    private void FadeIn()
    {
        Color targetColor = Color.white;

        if (targetColor == _backgroundImage.color && targetColor == _text.color)
        {
            return;
        }

        _backgroundImage.DOColor(targetColor, 0.1f)
            .SetEase(Ease.InQuad)
            .SetLink(gameObject);

        _text.DOColor(targetColor, 0.1f)
            .SetEase(Ease.InQuad)
            .SetLink(gameObject);
    }

    private void FadeOut()
    {
        Color targetColor = Color.white * 0.75f;
        targetColor.a = 1f;

        if (targetColor == _backgroundImage.color && targetColor == _text.color)
        {
            return;
        }

        _backgroundImage.DOColor(targetColor, 0.1f)
            .SetEase(Ease.OutQuad)
            .SetLink(gameObject);

        _text.DOColor(targetColor, 0.1f)
            .SetEase(Ease.OutQuad)
            .SetLink(gameObject);
    }
}