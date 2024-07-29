using MediatR;
using ShipmentService.IntegrationEvent;

namespace ShipmentService.Aplication.CQRS.Shipments.Commands.Update
{
    public record UpdateShipmentCommand
   (
       Guid ShipmentId,
       Guid OrderId,
       ShipmentStatus Status,
       DateTime UpdatedStatusDateTime,
       string Message
   ) : IRequest<Unit>;
}
