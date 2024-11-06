using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsPanel : Panel
{
    [SerializeField] private TopBar _topBar;
    [SerializeField] private Image _progressBarFiller;
    [SerializeField] private TextMeshProUGUI _levelNumberText;
    [SerializeField] private TextMeshProUGUI _experiencePointsText;
    [SerializeField] private RectTransform _circleRectTransform;

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

        _progressBarFiller.fillAmount = currentLevelPoints / maxLevelPoints;

        _circleRectTransform.eulerAngles = new Vector3(0f, 0f, currentLevelPoints / maxLevelPoints * -360f + 180f);

        _experiencePointsText.text = $"{currentLevelPoints} / {maxLevelPoints}";
    }
}