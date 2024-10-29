using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private void Start()
    {
        Storage.LoadInventory();
    }
}