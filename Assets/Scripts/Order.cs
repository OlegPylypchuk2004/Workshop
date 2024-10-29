public class Order
{
    private OrderResource[] _orderResources;

    public Order(OrderResource[] orderResources)
    {
        _orderResources = orderResources;
    }

    public OrderResource[] OrderResources => _orderResources;
}