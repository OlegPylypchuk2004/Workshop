using UnityEngine;

public class SafeArea : MonoBehaviour
{
    [System.Flags]
    public enum SafeAreaSides
    {
        None = 0,
        Top = 1 << 0,
        Bottom = 1 << 1,
        Right = 1 << 2,
        Left = 1 << 3
    }

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private SafeAreaSides _sides = SafeAreaSides.None;

    private void OnValidate()
    {
        _rectTransform ??= GetComponent<RectTransform>();
    }

    private void Awake()
    {
        Rect safeArea = Screen.safeArea;

        Vector2 anchorMin = Vector2.zero;
        Vector2 anchorMax = Vector2.one;

        if (_sides.HasFlag(SafeAreaSides.Top))
        {
            anchorMin.y = safeArea.yMin / Screen.height;
        }
        if (_sides.HasFlag(SafeAreaSides.Bottom))
        {
            anchorMax.y = safeArea.yMax / Screen.height;
        }
        if (_sides.HasFlag(SafeAreaSides.Right))
        {
            anchorMin.x = safeArea.xMin / Screen.width;
        }
        if (_sides.HasFlag(SafeAreaSides.Left))
        {
            anchorMax.x = safeArea.xMax / Screen.width;
        }

        _rectTransform.anchorMin = anchorMin;
        _rectTransform.anchorMax = anchorMax;
    }
}