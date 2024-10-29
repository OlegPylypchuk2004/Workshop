public class Order
{
    private string _customerName;
    private OrderResource[] _orderResources;

    public Order(string customerName, OrderResource[] orderResources)
    {
        _customerName = customerName;
        _orderResources = orderResources;
    }

    public string CustomerName => _customerName;
    public OrderResource[] OrderResources => _orderResources;
}