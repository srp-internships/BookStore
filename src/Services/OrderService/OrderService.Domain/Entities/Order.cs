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
    public decimal TotalPrice
    {
        get => Items.Sum(x => x.Price * x.Quantity);
        private set { }
    }
}
