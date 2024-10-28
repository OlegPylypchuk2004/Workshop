using UnityEngine;

public class ScrollReset : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    private void OnEnable()
    {
        _rectTransform.anchoredPosition = Vector3.zero;
    }
}