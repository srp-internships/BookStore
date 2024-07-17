namespace Orders.IntegrationEvents;

public sealed record OrderAnalyticRequestEvent(
    List<Book> Orders,
    Guid CustomerId,
    DateTime CreatedAt
);

