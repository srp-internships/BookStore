namespace OrderService.Domain.Events;

public sealed class OrderStatusUpdatedEvent(Order order) : IDomainEvent;