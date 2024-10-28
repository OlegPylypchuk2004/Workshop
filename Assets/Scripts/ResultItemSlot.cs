using TMPro;
using UnityEngine;

public class ResultItemSlot : ItemSlot
{
    [SerializeField] private TextMeshProUGUI _timeText;

    public void SetTime(float time = 0f)
    {
        if (time <= 0f)
        {
            _timeText.text = "";
            return;
        }

        _timeText.text = TextFormatter.FormatTime(time);
    }
}