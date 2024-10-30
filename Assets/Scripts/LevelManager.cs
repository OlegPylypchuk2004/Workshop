using UnityEngine;

public static class LevelManager
{
    private static int currentLevel = 1;

    static LevelManager()
    {
        PlayerDataManager.Data.ExperiencePointsChanged += OnExperiencePointsChanged;
        UpdateCurrentLevel(PlayerDataManager.Data.ExperiencePointsCount);
    }

    public static int GetCurrentLevel() => currentLevel;

    public static int GetPointsForNextLevel()
    {
        int pointsNeeded = CalculatePointsNeeded(currentLevel);
        int currentPoints = PlayerDataManager.Data.ExperiencePointsCount - CalculateTotalPointsForPreviousLevels(currentLevel);
        return Mathf.Max(pointsNeeded - currentPoints, 0);
    }

    public static int GetCurrentLevelPoints()
    {
        int totalPoints = PlayerDataManager.Data.ExperiencePointsCount;
        int previousLevelsPoints = CalculateTotalPointsForPreviousLevels(currentLevel);
        return Mathf.Max(totalPoints - previousLevelsPoints, 0);
    }

    private static void OnExperiencePointsChanged(int totalPoints) => UpdateCurrentLevel(totalPoints);

    private static void UpdateCurrentLevel(int totalPoints)
    {
        currentLevel = 1;
        while (totalPoints >= CalculatePointsNeeded(currentLevel))
        {
            totalPoints -= CalculatePointsNeeded(currentLevel++);
        }
    }

    private static int CalculatePointsNeeded(int level)
    {
        int basePoints = Resources.Load<GameRules>("GameRules").FirstLevelExperiencePointsCount;
        return level <= 1 ? basePoints : basePoints * (1 << (level - 1));
    }

    private static int CalculateTotalPointsForPreviousLevels(int level)
    {
        int totalPoints = 0;
        for (int i = 1; i < level; i++)
        {
            totalPoints += CalculatePointsNeeded(i);
        }
        return totalPoints;
    }
}