using System.Globalization;
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

    public static string FormatValue(float value)
    {
        if (value >= 1_000_000_000)
        {
            return (value / 1_000_000_000).ToString("F2", CultureInfo.InvariantCulture) + "B";
        }
        else if (value >= 1_000_000)
        {
            return (value / 1_000_000).ToString("F2", CultureInfo.InvariantCulture) + "M";
        }
        else if (value >= 1_000)
        {
            return (value / 1_000).ToString("F2", CultureInfo.InvariantCulture) + "k";
        }
        else
        {
            return value.ToString("F0", CultureInfo.InvariantCulture);
        }
    }
}