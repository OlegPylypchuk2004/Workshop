using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private bool _isLoadNextScene;

    private void Start()
    {
        Storage.LoadInventory();

        if (_isLoadNextScene)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

#if UNITY_EDITOR
            Application.targetFrameRate = 1000;
#else
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.numerator;
#endif
        }
    }
}