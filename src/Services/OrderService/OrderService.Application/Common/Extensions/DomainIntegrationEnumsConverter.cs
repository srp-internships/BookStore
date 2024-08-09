namespace OrderService.Application.Common.Extensions;

public static class DomainIntegrationEnumsConverter
{
    public static OrderStatus ToIntegrationEnum(this OrderService.Domain.Enums.OrderStatus status)
    {
        return status switch
        {
            OrderService.Domain.Enums.OrderStatus.Failed => OrderStatus.Failed,
            OrderService.Domain.Enums.OrderStatus.Completed => OrderStatus.Completed,
            OrderService.Domain.Enums.OrderStatus.PaymentProcessing => OrderStatus.PaymentProcessing,
            OrderService.Domain.Enums.OrderStatus.ShipmentProcessing => OrderStatus.ShipmentProcessing,
            _ => throw new InvalidCastException($"Not matchable domain enum value detected: {status}"),
        };
    }

    public static OrderService.Domain.Enums.OrderStatus ToDomainEnum(this OrderStatus status)
    {
        return status switch
        {
            OrderStatus.Failed => OrderService.Domain.Enums.OrderStatus.Failed,
            OrderStatus.Completed => OrderService.Domain.Enums.OrderStatus.Completed,
            OrderStatus.PaymentProcessing => OrderService.Domain.Enums.OrderStatus.PaymentProcessing,
            OrderStatus.ShipmentProcessing => OrderService.Domain.Enums.OrderStatus.ShipmentProcessing,
            _ => throw new InvalidCastException($"Not matchable domain enum value detected: {status}"),
        };
    }
}
