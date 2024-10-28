using UnityEngine;

public static class TextFormatter
{
    public static string FormatTime(float timeInSeconds)
    {
        if (timeInSeconds < 0f)
        {
            timeInSeconds = 0f;
        }

        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        if (timeInSeconds >= 60)
        {
            return $"{minutes}m {seconds}s";
        }
        else
        {
            return $"{seconds}s";
        }
    }
}