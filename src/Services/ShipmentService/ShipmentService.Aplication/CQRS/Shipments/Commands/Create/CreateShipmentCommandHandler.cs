using AutoMapper;
using MediatR;
using ShipmentService.Aplication.Interfaces;
using ShipmentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.CQRS.Shipments.Commands.Create
{
    public class CreateShipmentCommandHandler : IRequestHandler<CreateShipmentCommand, Guid>
    {
        private readonly IShipmentService _shipmentService;

        public CreateShipmentCommandHandler(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        public async Task<Guid> Handle(CreateShipmentCommand request, CancellationToken cancellationToken)
        {
            var shipment = new Shipment
            {
                ShipmentId = Guid.NewGuid(),
                OrderId = request.OrderId,
                CustomerId = request.CustomerId,
                ShippingAddress = request.ShippingAddress,
                Items = request.Items,
                Status = Status.Pending,
                EstimatedDeliveryDate = DateTime.UtcNow.AddDays(5)
            };

            await _shipmentService.CreateShipmentAsync(shipment);

            return shipment.ShipmentId;
        }
    }
}
