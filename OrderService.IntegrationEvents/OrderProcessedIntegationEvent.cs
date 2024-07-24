namespace OrderService.IntegrationEvents;

public record OrderProcessedIntegrationEvent(Guid Id, Guid CustomerId, Address ShippingAddress, 
    Payment Payment,string Status, List<OrderItem> OrderItems, decimal TotalPrice);


public record Payment(string? CardName, string CardNumber, string Expiration, string CVV, int PaymentMethod);

public record Address(string FirstName, string LastName, string EmailAddress, string AddressLine, string Country, string State);

public record OrderItem (Guid OrderId,Guid BookId, int Quantity, decimal Price);



