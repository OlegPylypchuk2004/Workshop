using UnityEngine;

public class LevelUpChecker : MonoBehaviour
{
    [SerializeField] private NewLevelPanel _newLevelPanel;

    private int _cashedLevel;

    private void Awake()
    {
        _cashedLevel = LevelManager.GetCurrentLevel();
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
        int level = LevelManager.GetCurrentLevel();

        if (_cashedLevel < level)
        {
            _cashedLevel = level;

            Invoke(nameof(OpenNewLevelPanel), 0.5f);
        }
    }

    private void OpenNewLevelPanel()
    {
        NavigationController.Instance.OpenPanel(_newLevelPanel);
    }
}