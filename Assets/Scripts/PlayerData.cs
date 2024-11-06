using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    private int _creditsCount = 10000;
    private int _experiencePointsCount = 0;
    private List<EquipmentData> _purchasedEquipments = new List<EquipmentData>();

    public event Action<int> CreditsCountChanged;
    public event Action<int> CreditsCountIncreased;
    public event Action<int> CreditsCountDecreased;
    public event Action<int> ExperiencePointsChanged;
    public event Action<int> ExperiencePointsIncreased;
    public event Action<List<EquipmentData>> PurchasedEquipmentsChanged;

    public int CreditsCount
    {
        get => _creditsCount;
        set
        {
            if (_creditsCount != value)
            {
                int result = value - _creditsCount;

                if (result > 0)
                {
                    CreditsCountIncreased?.Invoke(result);
                }
                else if (result < 0)
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
            int result = value - _experiencePointsCount;

            if (_experiencePointsCount != value)
            {
                _experiencePointsCount = value;
                ExperiencePointsChanged?.Invoke(_experiencePointsCount);
                ExperiencePointsIncreased?.Invoke(result);
            }
        }
    }

    public void AddEquipmentToPurchasingEquipmentsList(EquipmentData data)
    {
        if (data == null || _purchasedEquipments.Contains(data))
        {
            return;
        }

        _purchasedEquipments.Add(data);

        PurchasedEquipmentsChanged?.Invoke(_purchasedEquipments);
    }

    public bool IsPurchasedEquipment(EquipmentData data)
    {        
        return _purchasedEquipments.Contains(data);
    }

    public void ResetToDefaults()
    {
        CreditsCount = 10000;
        ExperiencePointsCount = 0;
        _purchasedEquipments = new List<EquipmentData>();
    }
}