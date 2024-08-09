namespace OrderService.Domain.Entities;

public class Order : BaseEntity
{
    public OrderStatus Status { get; set; } = OrderStatus.PaymentProcessing;
    public Shipment? Shipment { get; set; }
    public Payment? Payment { get; set; }
    public Guid CustomerId { get; set; }
    public Guid? CartId { get; set; }
    public Address ShippingAddress { get; set; } = new Address();
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    public decimal TotalPrice
    {
        get => Items?.Sum(x => x.Price * x.Quantity) ?? 0;
        private set { }

    }
}
