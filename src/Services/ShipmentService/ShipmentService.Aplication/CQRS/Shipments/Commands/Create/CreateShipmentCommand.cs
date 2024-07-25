using MediatR;
using ShipmentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.CQRS.Shipments.Commands.Create
{
    public record CreateShipmentCommand
   (
       Guid OrderId,
       Guid CustomerId,
       ShippingAddress ShippingAddress,
       List<ShipmentItem> Items
   ) : IRequest<Guid>;
}
