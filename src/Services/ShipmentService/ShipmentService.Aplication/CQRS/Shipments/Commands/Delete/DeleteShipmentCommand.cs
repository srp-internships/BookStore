using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.CQRS.Shipments.Commands.Delete
{
    public class DeleteShipmentCommand: IRequest<string>
    {
        public Guid ShipmentId { get; set; }
    }
}
