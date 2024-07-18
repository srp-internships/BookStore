namespace OrderService.Domain.Events;

public sealed record OrderCompletedEvent(
    List<Book> Books,
    Guid CustomerId,
    DateTime CreatedAt
);