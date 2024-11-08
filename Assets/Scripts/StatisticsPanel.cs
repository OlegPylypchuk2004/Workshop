using DG.Tweening;
using TMPro;
using UnityEngine;

public class StatisticsPanel : Panel
{
    [SerializeField] private TopBar _topBar;
    [SerializeField] private TextMeshProUGUI _levelNumberText;
    [SerializeField] private TextMeshProUGUI _experiencePointsText;
    [SerializeField] private RadialProgressBar _radialProgressBar;

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

        _radialProgressBar.UpdateView(0f, 1f);

        float currentValue = 0f;

        DOTween.To(() => currentValue, x => currentValue = x, currentLevelPoints, 0.5f)
            .SetEase(Ease.OutQuad)
            .OnUpdate(() =>
            {
                _radialProgressBar.UpdateView(currentValue, maxLevelPoints);
                _experiencePointsText.text = $"{Mathf.RoundToInt(currentValue)} / {maxLevelPoints}";
            });
    }
}