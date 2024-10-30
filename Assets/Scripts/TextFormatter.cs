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

        int days = Mathf.FloorToInt(timeInSeconds / 86400);
        int hours = Mathf.FloorToInt((timeInSeconds % 86400) / 3600);
        int minutes = Mathf.FloorToInt((timeInSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        string result = "";

        if (days > 0)
        {
            result += $"{days}d ";
        }
        if (hours > 0)
        {
            result += $"{hours}h ";
        }
        if (minutes > 0)
        {
            result += $"{minutes}m ";
        }
        if (seconds > 0 || result == "")
        {
            result += $"{seconds}s";
        }

        return result.Trim();
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