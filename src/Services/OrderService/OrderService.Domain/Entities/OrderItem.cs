namespace OrderService.Domain.Entities;

public class OrderItem : Entity<OrderItemId>
{
    private OrderItem() { }

    internal OrderItem(OrderId orderId, BookId bookId, int quantity, decimal price)
    {
        Id = OrderItemId.Of(Guid.NewGuid());
        OrderId = orderId;
        BookId = bookId;
        Quantity = quantity;
        Price = price;
    }

    public OrderId OrderId { get; private set; } = default!;
    public BookId BookId { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
}
