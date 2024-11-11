using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoadingTransition : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextDotsAnimator _textDotsAnimator;

    public static SceneLoadingTransition Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneLoader.Instance.LoadingStarted += ShowTransition;
    }

    private void OnDestroy()
    {
        SceneLoader.Instance.LoadingStarted -= ShowTransition;
    }

    public void ShowTransition()
    {
        Sequence transitionSequence = DOTween.Sequence();

        transitionSequence.AppendCallback(() =>
        {
            if (UILocker.Instance != null)
            {
                UILocker.Instance.Lock();
            }

            _backgroundImage.color = new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, 0f);
            _canvasGroup.alpha = 0f;
            _slider.value = 0f;

            _textDotsAnimator.PlayAnimation();
        });

        transitionSequence.Append(_backgroundImage.DOFade(1f, 0.125f)
            .SetEase(Ease.OutQuad));

        transitionSequence.AppendInterval(0.125f / 2);

        transitionSequence.Append(_canvasGroup.DOFade(1f, 0.125f)
            .SetEase(Ease.OutQuad));

        transitionSequence.AppendInterval(0.125f / 2);

        transitionSequence.Append(_slider.DOValue(1f, 2f)
            .SetEase(Ease.InQuad));

        transitionSequence.AppendInterval(0.125f / 2);

        transitionSequence.Append(_canvasGroup.DOFade(0f, 0.125f)
            .SetEase(Ease.InQuad));

        transitionSequence.AppendInterval(0.125f / 2);

        transitionSequence.Append(_backgroundImage.DOFade(0f, 0.125f)
            .SetEase(Ease.InQuad));

        transitionSequence.SetLink(gameObject);

        transitionSequence.OnComplete(() =>
        {
            if (UILocker.Instance != null)
            {
                UILocker.Instance.Unlock();
            }

            _textDotsAnimator.StopAnimation();
        });
    }
}