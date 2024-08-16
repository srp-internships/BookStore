using MediatR;
using ShipmentService.IntegrationEvent;

namespace ShipmentService.Aplication.CQRS.Shipments.Commands.Update
{
    public record UpdateShipmentCommand
   (
       Guid ShipmentId,
       ShipmentStatus Status
   ) : IRequest<Unit>;
}
