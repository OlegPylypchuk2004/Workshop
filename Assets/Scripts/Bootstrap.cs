using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    private void Start()
    {
        Storage.LoadInventory();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}