using MediatR;
using ShipmentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.CQRS.Shipments.Commands.Update
{
    public record UpdateShipmentCommand
   (
       Guid ShipmentId,
       Status Status,
       DateTime UpdatedStatusDateTime
   ) : IRequest<Unit>;
}
