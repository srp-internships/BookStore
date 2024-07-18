namespace OrderService.Domain.Entities;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal TotalPrice
    {
        get => OrderItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }

    public static Order Create(OrderId id, CustomerId customerId, Address shippingAddress, Address billingAddress, Payment payment)
    {
        var order = new Order
        {
            Id = id,
            CustomerId = customerId,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            Status = OrderStatus.Pending
        };

        order.AddDomainEvent(new OrderProcessedEvent(order));
        return order;
    }

    public void Update(Address shippingAddress, Address billingAddress, Payment payment, OrderStatus status)
    {
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = status;

        AddDomainEvent(new OrderStatusUpdatedEvent(this));
    }

    public void Add(BookId bookId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var orderItem = new OrderItem(Id, bookId, quantity, price);
        _orderItems.Add(orderItem);
    }

    public void Remove(BookId bookId)
    {
        var orderItem = _orderItems.FirstOrDefault(x => x.BookId == bookId);
        if (orderItem is not null)
        {
            _orderItems.Remove(orderItem);
        }
    }
}
