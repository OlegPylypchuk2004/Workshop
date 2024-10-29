using UnityEngine;

[CreateAssetMenu(fileName = "NewOrderData", menuName = "Data/Order")]
public class OrderData : ScriptableObject
{
    [field: SerializeField] public string CustomerName { get; private set; }
    [field: SerializeField] public int ExperiencePointsPerItem { get; private set; }
    [field: SerializeField] public ItemData[] Items { get; private set; }
}