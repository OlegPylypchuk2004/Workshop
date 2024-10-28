using System.IO;
using UnityEngine;

public static class PlayerDataManager
{
    private static readonly string filePath;

    static PlayerDataManager()
    {
        filePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
        Data = new PlayerData();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            Data = JsonUtility.FromJson<PlayerData>(json);
        }

        Data.CreditsCountChanged += credits => SaveData();
    }

    public static PlayerData Data { get; private set; }

    public static void SaveData()
    {
        string json = JsonUtility.ToJson(Data);
        File.WriteAllText(filePath, json);
    }

    public static void ResetToDefaults()
    {
        Data.ResetToDefaults();
        SaveData();
    }
}