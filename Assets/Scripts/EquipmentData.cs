using UnityEngine;

[CreateAssetMenu(fileName = "NewEquipmentData", menuName = "Data/Equipment")]
public class EquipmentData : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string IdleStateButtonText { get; private set; }
    [field: SerializeField] public string AtWorkStateButtonText { get; private set; }
    [field: SerializeField] public string DoneStateButtonText { get; private set; }
}