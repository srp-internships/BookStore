using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderService.IntegrationEvents;
using ShipmentService.Aplication.Common.Extentions;
using ShipmentService.Infrastructure.Persistence.DbContexts;

namespace ShipmentService.Infrastructure.Consumers
{
    public class OrderStatusUpdatedConsumer : IConsumer<OrderStatusUpdatedIntegrationEvent>
    {
        private readonly ILogger<OrderStatusUpdatedConsumer> _logger;
        private readonly ShipmentContext _context;

        public OrderStatusUpdatedConsumer(ILogger<OrderStatusUpdatedConsumer> logger, ShipmentContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Consume(ConsumeContext<OrderStatusUpdatedIntegrationEvent> context)
        {
            var statusUpdateEvent = context.Message;

            var shipment = await _context.Shipments
                .FirstOrDefaultAsync(s => s.OrderId == statusUpdateEvent.OrderId);

            if (shipment != null)
            {
                shipment.OrderStatus = OrderStatusConverter.ToShipmentOrderStatus(statusUpdateEvent.Status);
                if (statusUpdateEvent.Status == OrderStatus.ShipmentProcessing)
                    shipment.Status = Domain.Enums.Status.Pending;
                else if (statusUpdateEvent.Status == OrderStatus.Failed)
                    _context.Shipments.Remove(shipment);
                _context.Shipments.Update(shipment);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Updated shipment status for OrderId {statusUpdateEvent.OrderId} to {statusUpdateEvent.Status}");
            }
            else
            {
                _logger.LogWarning($"Shipment not found for OrderId {statusUpdateEvent.OrderId}");
            }
        }
    }
}
