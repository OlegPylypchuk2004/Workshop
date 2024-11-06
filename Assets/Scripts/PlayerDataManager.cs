using System;
using System.IO;
using UnityEngine;

public static class PlayerDataManager
{
    private static readonly string filePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
    private static readonly string inventoryFilePath = Path.Combine(Application.persistentDataPath, "InventoryData.json");

    static PlayerDataManager()
    {
        Data = new PlayerData();
        LoadData();
        AttachEventHandlers();
    }

    public static PlayerData Data { get; private set; }

    public static void SaveData()
    {
        string json = JsonUtility.ToJson(Data);
        File.WriteAllText(filePath, json);
    }

    private static void LoadData()
    {
        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                Data = JsonUtility.FromJson<PlayerData>(json);
            }
            catch (Exception exception)
            {
                Debug.LogError($"Error loading data: {exception.Message}");
                Data = new PlayerData();
            }
        }
    }

    private static void AttachEventHandlers()
    {
        Data.CreditsCountChanged += credits => SaveData();
        Data.ExperiencePointsChanged += experience => SaveData();
        Data.PurchasedEquipmentsChanged += equipments => SaveData();
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