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

        PlayerDataManager.Data.CreditsCountChanged += OnCreditsCountChanged;
    }

    private void OnDisable()
    {
        PlayerDataManager.Data.CreditsCountChanged -= OnCreditsCountChanged;
    }

    private void OnCreditsCountChanged(int count)
    {
        int currentCount = _cashedCreditsCount;
        _cashedCreditsCount = count;

        DOTween.To(() => currentCount, x => currentCount = x, count, 0.25f)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                _countText.text = TextFormatter.FormatValue(currentCount);
            });
    }
}