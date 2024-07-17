using OrderService.Domain.Entities;

namespace OrderService.IntegrationEvents;

public sealed record OrderCompletedEvent(
    List<Book> Books,
    Guid CustomerId,
    DateTime CreatedAt
);