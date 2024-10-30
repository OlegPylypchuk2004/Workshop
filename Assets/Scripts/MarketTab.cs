using System.Collections.Generic;
using UnityEngine;

public class MarketTab : Tab
{
    [SerializeField] private MarketBuyItemPanel _panelPrefab;
    [SerializeField] private TopBar _topBar;
    [SerializeField] private RectTransform _panelsListRecTransform;
    [SerializeField] private SetCountPanel _setCountPanel;

    private List<MarketBuyItemPanel> _panels = new List<MarketBuyItemPanel>();
    private List<MarketItem> _goods = new List<MarketItem>();
    private ItemData[] _itemDatas;
    private MarketItem _chosenMarketItem;

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
        _chosenMarketItem = panel.MarketItem;

        int maxItemsCount = Mathf.CeilToInt(PlayerDataManager.Data.CreditsCount / panel.MarketItem.Price);

        if (maxItemsCount <= 0)
        {
            _chosenMarketItem = null;
            return;
        }

        if (maxItemsCount == 1)
        {
            PlayerDataManager.Data.CreditsCount -= _chosenMarketItem.Price * 1;
            Storage.AddItem(_chosenMarketItem.ItemData, 1);

            _chosenMarketItem = null;
            return;
        }

        if (maxItemsCount > 25)
        {
            maxItemsCount = 25;
        }

        _setCountPanel.Initialize(1, maxItemsCount, true, _chosenMarketItem.Price);
        _setCountPanel.CountChosen += OnCountChosen;
        _setCountPanel.CountChooseCanceled += OnCountChooseCanceled;

        NavigationController.Instance.OpenPanel(_setCountPanel);
    }

    private void OnCountChosen(int count)
    {
        _setCountPanel.CountChosen -= OnCountChosen;
        _setCountPanel.CountChooseCanceled -= OnCountChooseCanceled;

        NavigationController.Instance.ClosePanel();

        PlayerDataManager.Data.CreditsCount -= _chosenMarketItem.Price * count;
        Storage.AddItem(_chosenMarketItem.ItemData, count);

        _chosenMarketItem = null;
    }

    private void OnCountChooseCanceled()
    {
        _setCountPanel.CountChosen -= OnCountChosen;
        _setCountPanel.CountChooseCanceled -= OnCountChooseCanceled;
    }
}