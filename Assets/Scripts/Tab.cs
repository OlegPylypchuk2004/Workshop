using UnityEngine;

public class Tab : MonoBehaviour
{
    public virtual void Open()
    {
        Debug.Log(name);
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}