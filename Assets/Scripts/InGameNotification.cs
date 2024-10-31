using TMPro;
using UnityEngine;

public class InGameNotification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        Destroy(gameObject, 3);
    }

    public void Initialize(string text)
    {
        _text.text = text;
    }
}