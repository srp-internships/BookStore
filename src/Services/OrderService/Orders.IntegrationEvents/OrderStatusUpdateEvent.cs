namespace Orders.IntegrationEvents;

public sealed record OrderStatusUpdateEvent(
    Guid PaymentId,
    Guid OrderId,
    OrderStatus OrderStatus,
    string Message
    );
