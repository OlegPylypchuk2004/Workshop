using System.Collections.Generic;
using UnityEngine;

public static class Storage
{
    private static Dictionary<ItemData, int> items = new Dictionary<ItemData, int>();

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

        Debug.Log($"Added item: {itemData.Name}, {quantity}");
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

        Debug.Log($"Removed item: {itemData.Name}, {quantity}");
    }

    public static int GetItemQuantity(ItemData itemData)
    {
        return items.ContainsKey(itemData) ? items[itemData] : 0;
    }

    public static void ClearStorage()
    {
        items.Clear();
    }

    public static Dictionary<ItemData, int> GetAllItems()
    {
        return new Dictionary<ItemData, int>(items);
    }
}