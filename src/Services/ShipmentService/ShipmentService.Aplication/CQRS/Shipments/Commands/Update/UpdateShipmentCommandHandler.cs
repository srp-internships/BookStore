using AutoMapper;
using MassTransit;
using MediatR;
using ShipmentService.Aplication.Interfaces;
using ShipmentService.Aplication.Common.Extentions;
using ShipmentService.IntegrationEvent;
using ShipmentService.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace ShipmentService.Aplication.CQRS.Shipments.Commands.Update
{
    public class UpdateShipmentCommandHandler : IRequestHandler<UpdateShipmentCommand, Unit>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<UpdateShipmentCommandHandler> _logger;

        public UpdateShipmentCommandHandler(
            IShipmentRepository shipmentRepository,
            IMapper mapper,
            IPublishEndpoint publishEndpoint,
            ILogger<UpdateShipmentCommandHandler> logger)
        {
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateShipmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Проверка допустимых значений статуса
                if (!IsValidStatus(request.Status.ToDomainEnum()))
                {
                    _logger.LogError($"Invalid status value: {request.Status}");
                    throw new ArgumentOutOfRangeException(nameof(request.Status), "Invalid status value");
                }

                // Получение сущности доставки
                var shipment = await _shipmentRepository.GetShipmentByIdAsync(request.ShipmentId);
                if (shipment == null)
                {
                    _logger.LogError($"Shipment with ID {request.ShipmentId} not found.");
                    throw new Exception("Shipment not found");
                }

                // Сохранение предыдущего статуса
                var previousStatus = shipment.Status;

                // Обновление сущности с использованием AutoMapper
                _mapper.Map(request, shipment);

                // Сохранение изменений в репозитории
                await _shipmentRepository.UpdateShipmentAsync(shipment);
                await _shipmentRepository.SaveChangesAsync();

                // Проверка на изменение статуса и публикация события, если статус стал Shipped
                if (previousStatus != Status.Shipped && shipment.Status == Status.Shipped)
                {
                    var shipmentUpdatedEvent = new ShipmentUpdatedEvent(
                        ShipmentId: shipment.ShipmentId,
                        OrderId: shipment.OrderId,
                        Status: shipment.Status.ToIntegrationEnum(),
                        StatusChangedDateTime: DateTime.UtcNow,
                        Message: "Shipment status updated to Shipped"
                    );

                    await _publishEndpoint.Publish(shipmentUpdatedEvent);

                    _logger.LogInformation($"Published ShipmentUpdatedEvent for ShipmentId {shipment.ShipmentId}");
                }

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating shipment");
                throw;
            }
        }

        private bool IsValidStatus(ShipmentService.Domain.Enums.Status status)
        {
            return Enum.IsDefined(typeof(ShipmentService.Domain.Enums.Status), status);
        }
    }
}