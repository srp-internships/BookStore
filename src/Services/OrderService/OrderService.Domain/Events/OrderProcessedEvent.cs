namespace OrderService.Domain.Events;

public sealed class OrderProcessedEvent(Order order) : IDomainEvent;


