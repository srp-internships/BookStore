namespace Orders.IntegrationEvents;

public sealed record OrderShipmentRequestEvent(
    Guid OrderId,
    Guid CustomerId,
    ShippingAddress Address,
    List<OrderItem> Item
    );
