using UnityEngine;

[CreateAssetMenu(fileName = "NewGameRules", menuName = "Data/GameRules")]
public class GameRules : ScriptableObject
{
    [field: SerializeField, Range(1f, 2f)] public float OrderRewardInCreditsCoef { get; private set; }
}