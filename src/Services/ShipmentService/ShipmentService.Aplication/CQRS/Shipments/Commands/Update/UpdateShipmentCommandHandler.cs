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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<UpdateShipmentCommandHandler> _logger;

        public UpdateShipmentCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IPublishEndpoint publishEndpoint,
            ILogger<UpdateShipmentCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateShipmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling UpdateShipmentCommand for ShipmentId: {request.ShipmentId}");

                if (!IsValidStatus(request.Status.ToDomainEnum()))
                {
                    _logger.LogError($"Invalid status value: {request.Status}");
                    throw new ArgumentOutOfRangeException(nameof(request.Status), "Invalid status value");
                }

                var shipment = await _unitOfWork.Shipments.GetShipmentByIdAsync(request.ShipmentId);

                if (shipment == null)
                {
                    _logger.LogError($"Shipment with ID {request.ShipmentId} not found.");
                    throw new KeyNotFoundException("Shipment not found");
                }

                var previousStatus = shipment.Status;

                _mapper.Map(request, shipment);

                _logger.LogInformation($"Updating shipment: {shipment.ShipmentId}, Status: {shipment.Status}");

                 await _unitOfWork.Shipments.UpdateShipmentAsync(shipment);

                _logger.LogInformation("Saving changes to the database");

                var result = await _unitOfWork.CompleteAsync();

                if (result <= 0)
                {
                    _logger.LogError("No changes were saved to the database");
                    throw new Exception("Failed to save changes to the database");
                }

                _logger.LogInformation("Changes saved to the database");

                if (previousStatus != Status.Delivered && shipment.Status == Status.Delivered)
                {
                    var shipmentUpdatedEvent = new ShipmentUpdatedEvent(
                        ShipmentId: shipment.ShipmentId,
                        OrderId: shipment.OrderId,
                        Status: shipment.Status.ToIntegrationEnum(),
                        StatusChangedDateTime: DateTime.UtcNow,
                        Message: $"Shipment status updated to {shipment.Status.ToIntegrationEnum()}"
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
