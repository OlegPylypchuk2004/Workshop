using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField] private int _creditsCount = 10000;
    [SerializeField] private int _experiencePointsCount = 0;

    public event Action<int> CreditsCountChanged;
    public event Action<int> CreditsCountIncreased;
    public event Action<int> CreditsCountDecreased;
    public event Action<int> ExperiencePointsChanged;

    public int CreditsCount
    {
        get => _creditsCount;
        set
        {
            if (_creditsCount != value)
            {
                int result = value - _creditsCount;

                if (result > _creditsCount)
                {
                    CreditsCountIncreased?.Invoke(result);
                }
                else if (result < _creditsCount)
                {
                    CreditsCountDecreased?.Invoke(result);
                }

                _creditsCount = value;
                CreditsCountChanged?.Invoke(_creditsCount);
            }
        }
    }

    public int ExperiencePointsCount
    {
        get => _experiencePointsCount;
        set
        {
            if (_experiencePointsCount != value)
            {
                _experiencePointsCount = value;
                ExperiencePointsChanged?.Invoke(_experiencePointsCount);
            }
        }
    }

    public void ResetToDefaults()
    {
        CreditsCount = 10000;
        _experiencePointsCount = 0;
    }
}