using UnityEngine;
using UnityEngine.UI;

public class RadialProgressBar : MonoBehaviour
{
    [SerializeField] private Image _progressBarFiller;
    [SerializeField] private RectTransform _circleRectTransform;
    [SerializeField] private RectTransform _circleRectTransformBackground;

    public void UpdateView(float currentValue, float maxValue)
    {
        _progressBarFiller.fillAmount = currentValue / maxValue;

        _circleRectTransform.eulerAngles = new Vector3(0f, 0f, currentValue / maxValue * -360f + 180f);
        _circleRectTransformBackground.localEulerAngles = new Vector3(0f, 0f, _circleRectTransform.eulerAngles.z * -1f);
    }
}