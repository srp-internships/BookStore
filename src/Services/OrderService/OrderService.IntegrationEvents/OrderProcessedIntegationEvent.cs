namespace OrderService.IntegrationEvents;

public record OrderProcessedIntegrationEvent(Guid OrderId, Guid CustomerId, Address ShippingAddress,
    OrderStatus Status, List<OrderItem> Items, decimal TotalPrice);

public record Address(string FirstName, string LastName, string EmailAddress, string Country, string State, string Street);

public record OrderItem(Guid BookId, Guid SellerId, int Quantity, decimal Price);


public enum OrderStatus
{
    Completed,
    Failed,
    PaymentProcessing,
    ShipmentProcessing
}

