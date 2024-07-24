namespace OrderService.IntegrationEvents;

public record OrderProcessedIntegrationEvent(Guid Id, Guid CustomerId, Address ShippingAddress, 
    string Status, List<OrderItem> OrderItems, decimal TotalPrice);

public record Address(string FirstName, string LastName, string EmailAddress, string Country, string State);

public record OrderItem (Guid OrderId,Guid BookId, int Quantity, decimal Price);



