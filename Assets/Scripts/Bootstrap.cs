using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private void Start()
    {
#if UNITY_EDITOR
        Application.targetFrameRate = 1000;
#else
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.numerator;
#endif

        Storage.LoadInventory();

        SceneLoader.Instance.LoadByName("MainScene");
    }
}