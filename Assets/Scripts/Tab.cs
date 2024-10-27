using UnityEngine;

public class Tab : MonoBehaviour
{
    public virtual void Open()
    {
        if (isActiveAndEnabled)
        {
            return;
        }

        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        if (!isActiveAndEnabled)
        {
            return;
        }

        gameObject.SetActive(false);
    }
}