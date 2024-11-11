using System.Collections;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private IEnumerator Start()
    {
#if UNITY_EDITOR
        Application.targetFrameRate = 1000;
#else
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.numerator;
#endif

        Storage.LoadInventory();

        yield return new WaitForSeconds(0.125f);

        SceneLoader.Instance.LoadByName("MainScene");
    }
}