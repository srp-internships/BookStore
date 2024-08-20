namespace OrderService.Application.Common.Extensions;

public static class DomainIntegrationEnumsConverter
{
    public static OrderStatus ToIntegrationEnum(this OrderStatus status)
    {
        return status switch
        {
            OrderStatus.Failed => OrderStatus.Failed,
            OrderStatus.Completed => OrderStatus.Completed,
            OrderStatus.PaymentProcessing => OrderStatus.PaymentProcessing,
            OrderStatus.ShipmentProcessing => OrderStatus.ShipmentProcessing,
            _ => throw new InvalidCastException($"Not matchable domain enum value detected: {status}"),
        };
    }

    public static OrderStatus ToDomainEnum(this OrderStatus status)
    {
        return status switch
        {
            OrderStatus.Failed => OrderStatus.Failed,
            OrderStatus.Completed => OrderStatus.Completed,
            OrderStatus.PaymentProcessing => OrderStatus.PaymentProcessing,
            OrderStatus.ShipmentProcessing => OrderStatus.ShipmentProcessing,
            _ => throw new InvalidCastException($"Not matchable domain enum value detected: {status}"),
        };
    }
}
