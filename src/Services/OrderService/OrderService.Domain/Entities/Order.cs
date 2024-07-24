namespace OrderService.Domain.Entities;

public class Order : BaseEntity
{
    public OrderStatus Status { get; private set; } = OrderStatus.Draft;
    public DateTime OrderDate { get; private set; } = DateTime.Now;
    public Customer Customer { get; private set; } = default!;
    public Guid CustomerId {  get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Payment Payment { get;  private set; } = default!;
    public List<OrderItem> Items { get;  set; } = default!;
    public decimal TotalPrice
    {
        get => Items.Sum(x => x.Price * x.Quantity);
        private set { }
    }
}
