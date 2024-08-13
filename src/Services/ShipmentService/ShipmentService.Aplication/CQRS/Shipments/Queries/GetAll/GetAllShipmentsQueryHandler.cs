using MediatR;
using ShipmentService.Aplication.Interfaces;
using ShipmentService.Domain.Entities.Shipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.CQRS.Shipments.Queries.GetAll
{
    public class GetAllShipmentsQueryHandler : IRequestHandler<GetShipmentsQuery, IEnumerable<Shipment>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllShipmentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Shipment>> Handle(GetShipmentsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Shipments.GetAllShipmentsAsync();
        }
    }
}
