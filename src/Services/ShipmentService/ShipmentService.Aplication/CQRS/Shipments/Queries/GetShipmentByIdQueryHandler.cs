using MediatR;
using ShipmentService.Aplication.Interfaces;
using ShipmentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.CQRS.Shipments.Queries
{
    public class GetShipmentByIdQueryHandler : IRequestHandler<GetShipmentByIdQuery, Shipment>
    {
        private readonly IShipmentService _shipmentService;

        public GetShipmentByIdQueryHandler(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        public Task<Shipment> Handle(GetShipmentByIdQuery request, CancellationToken cancellationToken)
        {
            return _shipmentService.GetShipmentByIdAsync(request.ShipmentId);
        }
    }
}
