using DG.Tweening;
using TMPro;
using UnityEngine;

public class StatisticsPanel : Panel
{
    [SerializeField] private TopBar _topBar;
    [SerializeField] private TextMeshProUGUI _levelNumberText;
    [SerializeField] private TextMeshProUGUI _experiencePointsText;
    [SerializeField] private RadialProgressBar _radialProgressBar;
    [SerializeField] private RadialProgressBar _targetRadialProgressBar;

    public override void Open()
    {
        base.Open();

        _topBar.SetTitleText("Statistics");

        UpdateView();
    }

    private void UpdateView()
    {
        _levelNumberText.text = $"{LevelManager.GetCurrentLevel()}";

        float currentLevelPoints = LevelManager.GetCurrentLevelPoints();
        float maxLevelPoints = LevelManager.GetPointsForNextLevel() + LevelManager.GetCurrentLevelPoints();

        _experiencePointsText.text = $"{0} / {maxLevelPoints}";

        _targetRadialProgressBar.UpdateView(currentLevelPoints, maxLevelPoints);

        _radialProgressBar.UpdateView(0f, 1f);

        float currentValue = 0f;

        DOTween.To(() => currentValue, x => currentValue = x, currentLevelPoints, 0.25f)
            .SetEase(Ease.OutQuad)
            .SetDelay(0.25f)
            .OnUpdate(() =>
            {
                _radialProgressBar.UpdateView(currentValue, maxLevelPoints);
                _experiencePointsText.text = $"{Mathf.RoundToInt(currentValue)} / {maxLevelPoints}";
            });
    }
}