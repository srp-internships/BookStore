using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.CQRS.Shipments.Commands.Delete
{
    public record DeleteShipmentCommand(Guid ShipmentId) : IRequest<Unit>;
}
