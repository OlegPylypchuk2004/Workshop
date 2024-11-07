using UnityEngine;

public class NewLevelPanel : Panel
{
    [SerializeField] private TopBar _topBar;

    public override void Open()
    {
        base.Open();

        _topBar.SetTitleText($"Level {LevelManager.GetCurrentLevel()} unlocked");
    }
}