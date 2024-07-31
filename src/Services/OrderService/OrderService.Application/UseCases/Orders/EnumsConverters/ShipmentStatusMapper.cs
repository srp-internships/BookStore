namespace OrderService.Application.Mappers;

public static class ShipmentStatusMapper
{
    public static OrderService.Domain.Enums.ShipmentStatus ToDomainShipmentStatus(ShipmentService.IntegrationEvent.ShipmentStatus status)
    {
        return status switch
        {
            ShipmentService.IntegrationEvent.ShipmentStatus.Pending => OrderService.Domain.Enums.ShipmentStatus.Pending,
            ShipmentService.IntegrationEvent.ShipmentStatus.Delivered => OrderService.Domain.Enums.ShipmentStatus.Delivered,
            ShipmentService.IntegrationEvent.ShipmentStatus.Shipped => OrderService.Domain.Enums.ShipmentStatus.Shipped,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}
