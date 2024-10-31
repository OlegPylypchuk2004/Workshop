using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Storage
{
    private static Dictionary<ItemData, int> items = new Dictionary<ItemData, int>();
    private static readonly string filePath = Path.Combine(Application.persistentDataPath, "InventoryData.json");

    public static event Action<ItemData, int> ItemAdded;
    public static event Action<ItemData, int> ItemRemoved;

    public static void AddItem(ItemData itemData, int quantity = 1)
    {
        if (items.ContainsKey(itemData))
        {
            items[itemData] += quantity;
        }
        else
        {
            items[itemData] = quantity;
        }

        ItemAdded?.Invoke(itemData, quantity);

        SaveInventory();
    }

    public static void RemoveItem(ItemData itemData, int quantity)
    {
        if (items.ContainsKey(itemData))
        {
            items[itemData] -= quantity;
            if (items[itemData] <= 0)
            {
                items.Remove(itemData);
            }
        }

        ItemRemoved?.Invoke(itemData, quantity);

        SaveInventory();
    }

    public static int GetItemQuantity(ItemData itemData)
    {
        return items.ContainsKey(itemData) ? items[itemData] : 0;
    }

    public static void ClearStorage()
    {
        items.Clear();
        SaveInventory();
    }

    public static Dictionary<ItemData, int> GetAllItems()
    {
        return new Dictionary<ItemData, int>(items);
    }

    public static void SaveInventory()
    {
        InventoryData data = new InventoryData(items);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
    }

    public static void LoadInventory()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            InventoryData data = JsonUtility.FromJson<InventoryData>(json);
            items = data.ToDictionary();
        }
        else
        {
            items = new Dictionary<ItemData, int>();
        }
    }
}

[System.Serializable]
public class InventoryData
{
    public List<ItemEntry> items;

    public InventoryData(Dictionary<ItemData, int> itemsDict)
    {
        items = new List<ItemEntry>();
        foreach (var kvp in itemsDict)
        {
            items.Add(new ItemEntry(kvp.Key, kvp.Value));
        }
    }

    public Dictionary<ItemData, int> ToDictionary()
    {
        Dictionary<ItemData, int> dict = new Dictionary<ItemData, int>();
        foreach (var entry in items)
        {
            if (entry.ItemData != null)
            {
                dict[entry.ItemData] = entry.Quantity;
            }
        }
        return dict;
    }
}

[System.Serializable]
public class ItemEntry
{
    public ItemData ItemData;
    public int Quantity;

    public ItemEntry(ItemData itemData, int quantity)
    {
        ItemData = itemData;
        Quantity = quantity;
    }
}