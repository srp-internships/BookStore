using AutoMapper;
using MediatR;
using ShipmentService.Aplication.Interfaces;
using ShipmentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.CQRS.Shipments.Commands.Update
{
    public class UpdateShipmentCommandHandler : IRequestHandler<UpdateShipmentCommand, Unit>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;

        public UpdateShipmentCommandHandler(IShipmentRepository shipmentRepository, IMapper mapper)
        {
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateShipmentCommand request, CancellationToken cancellationToken)
        {
            var shipment = await _shipmentRepository.GetShipmentByIdAsync(request.ShipmentId);
            if (shipment == null)
            {
                throw new Exception("Shipment not found");
            }
            _mapper.Map(request, shipment);

            await _shipmentRepository.UpdateShipmentAsync(shipment);
            await _shipmentRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }

}
