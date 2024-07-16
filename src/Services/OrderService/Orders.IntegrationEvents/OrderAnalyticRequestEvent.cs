namespace Orders.IntegrationEvents;

public sealed record OrderAnalyticRequestEvent(
    List<Book> Books,
    Guid CustomerId,
    DateTime CreatedAt
);

