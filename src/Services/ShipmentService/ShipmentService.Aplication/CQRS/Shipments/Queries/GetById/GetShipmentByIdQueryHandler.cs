using MediatR;
using ShipmentService.Aplication.Interfaces;
using ShipmentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.CQRS.Shipments.Queries.GetById
{
    public class GetShipmentByIdQueryHandler : IRequestHandler<GetShipmentByIdQuery, Shipment>
    {
        private readonly IShipmentRepository _shipmentRepository;

        public GetShipmentByIdQueryHandler(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<Shipment> Handle(GetShipmentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _shipmentRepository.GetShipmentByIdAsync(request.ShipmentId);
        }
    }
}
