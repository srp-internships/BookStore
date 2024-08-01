

namespace ShipmentService.IntegrationEvent
{
    public sealed record ShipmentUpdatedEvent
    (
        Guid ShipmentId,
        Guid OrderId,
        ShipmentStatus Status,
        DateTime StatusChangedDateTime,
        string? Message
    );
}
