using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelNumberText;
    [SerializeField] private Image _progressBarDifferenceFiller;
    [SerializeField] private Image _progressBarFiller;

    private Sequence _progressBarSequence;
    private float _lastProgressBarValue;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayerDataManager.Data.ExperiencePointsCount += 12;
        }

        Debug.Log($"{LevelManager.GetCurrentLevelPoints()} / {LevelManager.GetPointsForNextLevel() + LevelManager.GetCurrentLevelPoints()}");
    }

    private void Start()
    {
        _levelNumberText.text = $"{LevelManager.GetCurrentLevel()}";

        float currentLevelPoints = LevelManager.GetCurrentLevelPoints();
        float maxLevelPoints = LevelManager.GetPointsForNextLevel() + LevelManager.GetCurrentLevelPoints();

        _progressBarDifferenceFiller.fillAmount = currentLevelPoints / maxLevelPoints;
        _progressBarFiller.fillAmount = currentLevelPoints / maxLevelPoints;
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
            _progressBarDifferenceFiller.fillAmount = currentLevelPoints / maxLevelPoints;
        });

        _progressBarSequence.Append(_progressBarFiller.DOFillAmount(currentLevelPoints / maxLevelPoints, 0.25f)
            .SetEase(Ease.OutQuad));

        _progressBarSequence.SetLink(gameObject);
    }
}