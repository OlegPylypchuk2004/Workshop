using System.IO;
using UnityEngine;

public static class PlayerDataManager
{
    private static readonly string filePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
    private static readonly string inventoryFilePath = Path.Combine(Application.persistentDataPath, "InventoryData.json");

    static PlayerDataManager()
    {
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

    public static void ClearAllData()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        if (File.Exists(inventoryFilePath))
        {
            File.Delete(inventoryFilePath);
        }

        Data.ResetToDefaults();
        Storage.ClearStorage();
    }
}