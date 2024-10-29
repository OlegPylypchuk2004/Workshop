using UnityEngine;

[CreateAssetMenu(fileName = "NewGameRules", menuName = "Data/GameRules")]
public class GameRules : ScriptableObject
{
    [field: SerializeField, Range(1f, 2f)] public float MarketPricesCoef { get; private set; }
    [field: SerializeField, Range(1f, 2f)] public float OrderRewardInCreditsCoef { get; private set; }
    [field: SerializeField, Range(1f, 2f)] public float OrderRewardInExperiencePointsCoef { get; private set; }
    [field: SerializeField, Range(1f, 2f)] public float NextLevelExperiencePointsCountCoef { get; private set; }
}