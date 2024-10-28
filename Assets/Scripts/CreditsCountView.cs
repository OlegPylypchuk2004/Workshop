using DG.Tweening;
using TMPro;
using UnityEngine;

public class CreditsCountView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;

    private int _animatedCreditsCount;

    private void OnEnable()
    {
        int numberOfCoins = PlayerDataManager.Data.CreditsCount;

        _animatedCreditsCount = numberOfCoins;
        _countText.text = numberOfCoins.ToString();

        PlayerDataManager.Data.CreditsCountChanged += OnNumberOfCoinsChanged;
    }

    private void OnDisable()
    {
        PlayerDataManager.Data.CreditsCountChanged -= OnNumberOfCoinsChanged;
    }

    private void OnNumberOfCoinsChanged(int numberOfCoins)
    {
        int currentCoinsAmount = _animatedCreditsCount;
        _animatedCreditsCount = numberOfCoins;

        DOTween.To(() => currentCoinsAmount, x => currentCoinsAmount = x, numberOfCoins, 0.25f)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                _countText.text = currentCoinsAmount.ToString();
            });
    }
}