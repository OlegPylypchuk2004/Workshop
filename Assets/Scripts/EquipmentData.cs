using UnityEngine;

[CreateAssetMenu(fileName = "NewEquipmentData", menuName = "Data/Equipment")]
public class EquipmentData : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
}