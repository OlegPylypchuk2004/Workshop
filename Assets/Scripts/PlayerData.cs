using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField] private int creditsCount = 10000;

    public event Action<int> CreditsCountChanged;

    public int CreditsCount
    {
        get => creditsCount;
        set
        {
            if (creditsCount != value)
            {
                creditsCount = value;
                CreditsCountChanged?.Invoke(creditsCount);
            }
        }
    }

    public void ResetToDefaults()
    {
        CreditsCount = 10000;
    }
}