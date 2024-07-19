namespace OrderService.Domain.Events;

public sealed record OrderStatusUpdatedEvent(Order order) : IDomainEvent;