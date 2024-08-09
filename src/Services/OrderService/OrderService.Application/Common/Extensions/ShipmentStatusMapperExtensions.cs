namespace OrderService.Application.Common.Extensions;

public static class ShipmentStatusMapperExtensions
{
    public static ShipmentStatus ToDomainShipmentStatus(this ShipmentService.IntegrationEvent.ShipmentStatus status)
    {
        return status switch
        {
            ShipmentService.IntegrationEvent.ShipmentStatus.Pending => ShipmentStatus.Pending,
            ShipmentService.IntegrationEvent.ShipmentStatus.Delivered => ShipmentStatus.Delivered,
            ShipmentService.IntegrationEvent.ShipmentStatus.Shipped => ShipmentStatus.Shipped,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}
