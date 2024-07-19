namespace OrderService.Domain.Events;

public sealed record OrderProcessedEvent(Order order) : IDomainEvent;


