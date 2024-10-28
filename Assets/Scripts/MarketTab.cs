using System.Collections.Generic;
using UnityEngine;

public class MarketTab : Tab
{
    [SerializeField] private MarketBuyItemPanel _panelPrefab;
    [SerializeField] private TopBar _topBar;
    [SerializeField] private RectTransform _panelsListRecTransform;

    private List<MarketBuyItemPanel> _panels = new List<MarketBuyItemPanel>();
    private ItemData[] _itemDatas;

    private void Start()
    {
        InitializeGoods();
    }

    private void InitializeGoods()
    {
        _itemDatas = Resources.LoadAll<ItemData>("Items");

        int goodsCount = Random.Range(10, 15);

        for (int i = 0; i < goodsCount; i++)
        {
            MarketBuyItemPanel panel = Instantiate(_panelPrefab, Vector3.zero, Quaternion.identity, _panelsListRecTransform);
            panel.Initialize(_itemDatas[Random.Range(0, _itemDatas.Length)], Random.Range(5, 25), Random.Range(1f, 1.75f));
            _panels.Add(panel);
        }
    }

    public override void Open()
    {
        base.Open();

        foreach (MarketBuyItemPanel panel in _panels)
        {
            panel.BuyButtonClicked += OnPanelBuyButtonClicked;
        }

        _topBar.SetTitleText("Market");
    }

    public override void Close()
    {
        base.Close();

        foreach (MarketBuyItemPanel panel in _panels)
        {
            panel.BuyButtonClicked -= OnPanelBuyButtonClicked;
        }
    }

    private void OnPanelBuyButtonClicked(MarketBuyItemPanel panel)
    {

    }
}