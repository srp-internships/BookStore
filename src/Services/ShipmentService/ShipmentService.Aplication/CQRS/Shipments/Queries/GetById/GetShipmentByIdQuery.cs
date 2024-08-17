using MediatR;
using ShipmentService.Domain.Entities.Shipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.CQRS.Shipments.Queries.GetById
{
    public record GetShipmentByIdQuery(Guid ShipmentId) : IRequest<Shipment>;
}
