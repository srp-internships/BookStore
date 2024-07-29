using AutoMapper;
using MassTransit;
using MediatR;
using ShipmentService.Aplication.Interfaces;
using ShipmentService.Aplication.Common.Extentions;
using ShipmentService.IntegrationEvent;
using ShipmentService.Domain.Enums;

namespace ShipmentService.Aplication.CQRS.Shipments.Commands.Update
{
    public class UpdateShipmentCommandHandler : IRequestHandler<UpdateShipmentCommand, Unit>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdateShipmentCommandHandler(IShipmentRepository shipmentRepository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
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

            // Преобразуйте статус к правильному типу перед вызовом метода расширения
            var integrationStatus = shipment.Status.ToIntegrationEnum();

            var shipmentUpdatedEvent = new ShipmentUpdatedEvent(
                ShipmentId: shipment.ShipmentId,
                OrderId: shipment.OrderId,
                Status: integrationStatus,
                StatusChangedDateTime: DateTime.Now,
                Message: "Shipment status updated"
            );

            await _publishEndpoint.Publish(shipmentUpdatedEvent);

            return Unit.Value;
        }
    }
}
