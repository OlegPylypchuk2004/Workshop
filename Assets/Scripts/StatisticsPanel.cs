using UnityEngine;

public class StatisticsPanel : Panel
{
    [SerializeField] private TopBar _topBar;

    public override void Open()
    {
        base.Open();

        _topBar.SetTitleText("Statistics");
    }
}