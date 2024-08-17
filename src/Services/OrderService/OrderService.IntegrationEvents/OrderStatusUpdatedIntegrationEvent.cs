namespace OrderService.IntegrationEvents;

public record OrderStatusUpdatedIntegrationEvent(Guid OrderId, OrderStatus Status);
