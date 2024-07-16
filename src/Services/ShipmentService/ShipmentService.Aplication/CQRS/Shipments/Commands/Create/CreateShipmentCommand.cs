using MediatR;
using ShipmentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.CQRS.Shipments.Commands.Create
{
    public class CreateShipmentCommand: IRequest<Guid>
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public List<ShipmentItem> Items { get; set; }
    }
}
