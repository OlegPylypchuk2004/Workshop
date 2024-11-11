using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private float _loadDelay;
    private AsyncOperation _loadSceneOperation;

    public event Action LoadingStarted;
    public event Action LoadingFinished;

    public static SceneLoader Instance { get; private set; }

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

        DontDestroyOnLoad(gameObject);
    }

    public void LoadByName(string sceneName)
    {
        LoadingStarted?.Invoke();

        _loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        _loadSceneOperation.allowSceneActivation = false;

        StartCoroutine(DelayBeforeLoad());
    }

    private IEnumerator DelayBeforeLoad()
    {
        yield return new WaitForSeconds(_loadDelay);

        _loadSceneOperation.allowSceneActivation = true;

        LoadingFinished?.Invoke();
    }
}