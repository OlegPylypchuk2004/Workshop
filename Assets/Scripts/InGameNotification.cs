using DG.Tweening;
using TMPro;
using UnityEngine;

public class InGameNotification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void Initialize(string text)
    {
        _text.text = text;

        _text.DOFade(0f, 0.25f)
            .From(1f)
            .SetEase(Ease.InQuad)
            .SetDelay(3f)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}