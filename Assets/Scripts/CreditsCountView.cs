using DG.Tweening;
using TMPro;
using UnityEngine;

public class CreditsCountView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;

    private int _cashedCreditsCount;

    private void OnEnable()
    {
        int numberOfCoins = PlayerDataManager.Data.CreditsCount;

        _cashedCreditsCount = numberOfCoins;
        _countText.text = TextFormatter.FormatValue(numberOfCoins);

        PlayerDataManager.Data.CreditsCountChanged += OnNumberOfCoinsChanged;
    }

    private void OnDisable()
    {
        PlayerDataManager.Data.CreditsCountChanged -= OnNumberOfCoinsChanged;
    }

    private void OnNumberOfCoinsChanged(int numberOfCoins)
    {
        int currentCoinsAmount = _cashedCreditsCount;
        _cashedCreditsCount = numberOfCoins;

        DOTween.To(() => currentCoinsAmount, x => currentCoinsAmount = x, numberOfCoins, 0.25f)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                _countText.text = TextFormatter.FormatValue(currentCoinsAmount);
            });
    }
}