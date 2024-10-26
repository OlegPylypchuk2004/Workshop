using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    [field: SerializeField] public string UniqueID { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}