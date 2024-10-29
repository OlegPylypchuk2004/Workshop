public class OrderResource
{
    private ItemData _itemData;
    private int _quantity;

    public OrderResource(ItemData itemData, int quantity)
    {
        _itemData = itemData;
        _quantity = quantity;
    }

    public ItemData ItemData => _itemData;
    public int Quantity => _quantity;
}