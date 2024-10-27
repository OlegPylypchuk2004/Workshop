using UnityEngine;
using UnityEngine.UI;

public class SetItemSlot : ItemSlot
{
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void OnClicked()
    {
        if (Input.GetKey(KeyCode.Backspace))
        {
            SetItem(null);
        }
        else if(Input.GetKey(KeyCode.O))
        {
            SetItem(Resources.Load<ItemData>("Items/iron_ore"));
        }
        else if(Input.GetKey(KeyCode.C))
        {
            SetItem(Resources.Load<ItemData>("Items/carbon"));
        }
        else if(Input.GetKey(KeyCode.I))
        {
            SetItem(Resources.Load<ItemData>("Items/iron"));
        }
    }
}