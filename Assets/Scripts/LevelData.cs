using UnityEngine;

[CreateAssetMenu (fileName = "NewLevelData", menuName = "Data/Level")]
public class LevelData : ScriptableObject
{
    [field: SerializeField] public ItemData[] NewItems { get; private set; }
    [field: SerializeField] public Recipe[] NewRecipes { get; private set; }
    [field: SerializeField] public EquipmentData[] NewEquipments { get; private set; }
}