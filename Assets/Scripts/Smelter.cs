using UnityEngine;

public class Smelter : MonoBehaviour
{
    [SerializeField] private SetItemSlot[] _setItemSlots;
    [SerializeField] private ResultItemSlot _resultItemSlot;

    private ItemData[] _itemDatas = new ItemData[2];

    private void OnEnable()
    {
        foreach (ItemSlot itemSlot in _setItemSlots)
        {
            itemSlot.ItemChanged += OnItemInSetSlotChanged;
        }
    }

    private void OnDisable()
    {
        foreach (ItemSlot itemSlot in _setItemSlots)
        {
            itemSlot.ItemChanged -= OnItemInSetSlotChanged;
        }
    }

    private void OnItemInSetSlotChanged(ItemData data)
    {
        if (data.Name == "iron ore")
        {
            _resultItemSlot.SetItem(Resources.Load<ItemData>("Items/iron"));
        }
    }
}