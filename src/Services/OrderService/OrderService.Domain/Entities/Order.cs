namespace OrderService.Domain.Entities;

public class Order : BaseEntity
{
    public OrderStatus Status { get; private set; } = OrderStatus.PaymentProcessing;
    public Shipment? Shipment { get; private set; }
    public Payment? Payment { get; private set; }
    public Guid CustomerId { get; private set; } = default!;
    public Guid? CartId { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public List<OrderItem> Items { get; set; } = default!;

    private decimal? _amount;

    public decimal TotalPrice
    {
        get
        {
            if (_amount == null)
            {
                _amount = Items?.Sum(x => x.Price * x.Quantity) ?? 0;
            }
            return (decimal)_amount;
        }
        private set
        {
            _amount = value;
        }
    }
}