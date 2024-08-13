using MediatR;
using ShipmentService.Aplication.Interfaces;
using ShipmentService.Domain.Entities.Shipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.CQRS.Shipments.Queries.GetById
{
    public class GetShipmentByIdQueryHandler : IRequestHandler<GetShipmentByIdQuery, Shipment>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetShipmentByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Shipment> Handle(GetShipmentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Shipments.GetShipmentByIdAsync(request.ShipmentId);
        }
    }
}
