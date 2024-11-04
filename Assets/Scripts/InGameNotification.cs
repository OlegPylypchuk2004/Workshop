using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameNotification : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _contentRectTransform;
    [SerializeField] private RectTransform _textRectTransform;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _creditsIcon;
    [SerializeField] private Image _experiencePointsIcon;

    public void Initialize(string text, bool isShowCreditsIcon = false, bool isShowExperiencePointsIcon = false, ColoredTextData[] coloredTexts = null)
    {
        if (coloredTexts == null || coloredTexts.Length == 0)
        {
            _text.text = text;
        }
        else
        {
            for (int i = 0; i < coloredTexts.Length; i++)
            {
                string finalText = text;

                foreach (ColoredTextData coloredText in coloredTexts)
                {
                    finalText = finalText.Replace(coloredText.Text, $"<color=#{ColorUtility.ToHtmlStringRGBA(coloredText.Color)}>{coloredText.Text}</color>");
                }

                _text.text = finalText;
            }
        }

        _creditsIcon.gameObject.SetActive(isShowCreditsIcon);
        _experiencePointsIcon.gameObject.SetActive(isShowExperiencePointsIcon);

        if (isShowCreditsIcon || isShowExperiencePointsIcon)
        {
            _textRectTransform.anchoredPosition = new Vector2(-287.5f, 0f);
        }
        else
        {
            _textRectTransform.anchoredPosition = new Vector2(-212.5f, 0f);
        }

        _contentRectTransform.anchoredPosition = new Vector2(500f, _rectTransform.anchoredPosition.y);
        _canvasGroup.alpha = 0f;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(_rectTransform.DOSizeDelta(new Vector2(_rectTransform.sizeDelta.x, 50f), 0.125f)
            .From(new Vector2(_rectTransform.sizeDelta.x, 0f))
            .SetEase(Ease.OutQuad));

        sequence.Join(_canvasGroup.DOFade(1f, 0.125f)
            .From(0f)
            .SetEase(Ease.OutQuad));

        sequence.Append(_contentRectTransform.DOAnchorPos(Vector2.zero, 0.25f)
            .SetEase(Ease.OutQuad));

        sequence.AppendInterval(3f);

        sequence.Append(_canvasGroup.DOFade(0f, 0.125f)
            .From(1f)
            .SetEase(Ease.InQuad));

        sequence.SetLink(gameObject);

        sequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void OnDestroy()
    {
        if (_canvasGroup != null)
        {
            _canvasGroup.DOKill();
        }
    }
}