using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextDotsAnimator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;

    private string _baseText;
    private Sequence _sequence;

    private void Awake()
    {
        _baseText = _textMesh.text;
    }

    public void PlayAnimation()
    {
        int dotCount = 0;

        _sequence = DOTween.Sequence();

        _sequence.Append(DOTween.To(() => dotCount, x => dotCount = x, 3, 0.375f * 3)
            .OnUpdate(() =>
            {
                _textMesh.text = _baseText + new string('.', dotCount);
            }));

        _sequence.SetLoops(-1);
    }

    public void StopAnimation()
    {
        _sequence.Kill();
    }
}