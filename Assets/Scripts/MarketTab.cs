using System.Collections.Generic;
using UnityEngine;

public class MarketTab : Tab
{
    [SerializeField] private MarketBuyItemPanel _panelPrefab;
    [SerializeField] private TopBar _topBar;
    [SerializeField] private RectTransform _panelsListRecTransform;

    private List<MarketBuyItemPanel> _panels = new List<MarketBuyItemPanel>();
    private List<MarketItem> _goods = new List<MarketItem>();
    private ItemData[] _itemDatas;

    private void Awake()
    {
        InitializeGoods();
    }

    private void InitializeGoods()
    {
        _itemDatas = Resources.LoadAll<ItemData>("Items/RawMaterials");
        int goodsCount = _itemDatas.Length;

        for (int i = 0; i < goodsCount; i++)
        {
            ItemData itemData = _itemDatas[i];
            float priceCoef = Resources.Load<GameRules>("GameRules").MarketPricesCoef;
            int price = Mathf.RoundToInt(itemData.Price * priceCoef);

            MarketItem marketItem = new MarketItem
            {
                ItemData = itemData,
                Price = price,
            };

            _goods.Add(marketItem);

            MarketBuyItemPanel panel = Instantiate(_panelPrefab, Vector3.zero, Quaternion.identity, _panelsListRecTransform);
            panel.Initialize(marketItem);
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
        MarketItem marketItem = panel.MarketItem;

        if (PlayerDataManager.Data.CreditsCount >= marketItem.Price)
        {
            PlayerDataManager.Data.CreditsCount -= marketItem.Price;
            Storage.AddItem(marketItem.ItemData, 1);
        }
    }
}