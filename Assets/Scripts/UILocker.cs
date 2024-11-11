using UnityEngine;
using UnityEngine.EventSystems;

public class UILocker : MonoBehaviour
{
    [SerializeField] private EventSystem _eventSystem;

    public static UILocker Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Lock()
    {
        _eventSystem.gameObject.SetActive(false);
    }

    public void Unlock()
    {
        _eventSystem.gameObject.SetActive(true);
    }
}