using DG.Tweening;
using UnityEngine;

public class CreditsIcon : MonoBehaviour
{
    [SerializeField] private RectTransform _reflectionRectTransform;

    private Vector2 _startReflectionPosition;

    private void Start()
    {
        _startReflectionPosition = _reflectionRectTransform.anchoredPosition;

        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(7.5f);

        sequence.Append(_reflectionRectTransform.DOAnchorPos(_startReflectionPosition * -1, 0.5f)
            .From(_startReflectionPosition)
            .SetEase(Ease.OutQuad));

        sequence.SetLink(gameObject);
        sequence.SetLoops(-1);
    }
}