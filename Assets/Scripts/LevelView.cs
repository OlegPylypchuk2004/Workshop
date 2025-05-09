using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _levelNumberText;
    [SerializeField] private Image _progressBarDifferenceFiller;
    [SerializeField] private Image _progressBarFiller;
    [SerializeField] private StatisticsPanel _statisticsPanel;

    private Sequence _progressBarSequence;
    private float _lastProgressBarValue;

    private void Start()
    {
        _levelNumberText.text = $"{LevelManager.GetCurrentLevel()}";

        float currentLevelPoints = LevelManager.GetCurrentLevelPoints();
        float maxLevelPoints = LevelManager.GetPointsForNextLevel() + LevelManager.GetCurrentLevelPoints();

        _progressBarDifferenceFiller.fillAmount = currentLevelPoints / maxLevelPoints;
        _progressBarFiller.fillAmount = currentLevelPoints / maxLevelPoints;

        _progressBarDifferenceFiller.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerDataManager.Data.ExperiencePointsChanged += OnExperiencePointsCountChanged;
    }

    private void OnDisable()
    {
        PlayerDataManager.Data.ExperiencePointsChanged -= OnExperiencePointsCountChanged;
    }

    private void OnExperiencePointsCountChanged(int pointsCount)
    {
        _levelNumberText.text = $"{LevelManager.GetCurrentLevel()}";

        PlayProgressBarAnimation();
    }

    private void PlayProgressBarAnimation()
    {
        _progressBarSequence.Kill();

        float currentLevelPoints = LevelManager.GetCurrentLevelPoints();
        float maxLevelPoints = LevelManager.GetPointsForNextLevel() + LevelManager.GetCurrentLevelPoints();

        if (_lastProgressBarValue > currentLevelPoints / maxLevelPoints)
        {
            _progressBarDifferenceFiller.fillAmount = 1;
        }
        else
        {
            _progressBarDifferenceFiller.fillAmount = currentLevelPoints / maxLevelPoints;
        }

        _lastProgressBarValue = currentLevelPoints / maxLevelPoints;

        _progressBarSequence = DOTween.Sequence();

        _progressBarSequence.AppendInterval(0.25f);

        _progressBarSequence.AppendCallback(() =>
        {
            _progressBarDifferenceFiller.gameObject.SetActive(true);
            _progressBarDifferenceFiller.fillAmount = currentLevelPoints / maxLevelPoints;
        });

        _progressBarSequence.Append(_progressBarFiller.DOFillAmount(currentLevelPoints / maxLevelPoints, 0.25f)
            .SetEase(Ease.OutQuad));

        _progressBarSequence.AppendCallback(() =>
        {
            _progressBarDifferenceFiller.gameObject.SetActive(false);
        });

        _progressBarSequence.SetLink(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        NavigationController.Instance.OpenPanel(_statisticsPanel);
    }
}