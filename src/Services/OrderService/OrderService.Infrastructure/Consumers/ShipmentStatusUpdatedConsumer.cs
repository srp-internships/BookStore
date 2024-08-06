using MassTransit;
using Microsoft.Extensions.Logging;
using OrderService.Application.Common.Extensions;
using OrderService.Domain.Entities;
using OrderService.IntegrationEvents;
using ShipmentService.IntegrationEvent;

namespace OrderService.Infrastructure.Consumers;

public class ShipmentStatusUpdatedConsumer : IConsumer<ShipmentUpdatedEvent>
{
    private readonly ILogger<ShipmentStatusUpdatedConsumer> _logger;
    private readonly ApplicationDbContext _context;
    IPublishEndpoint _publishEndpoint;

    public ShipmentStatusUpdatedConsumer(ILogger<ShipmentStatusUpdatedConsumer> logger,
        ApplicationDbContext context,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _context = context;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<ShipmentUpdatedEvent> context)
    {
        var message = context.Message;

        var shipmentStatus = ShipmentStatusMapperExtensions.ToDomainShipmentStatus(message.Status);

        var shipment = await _context.Shipments.FindAsync(message.OrderId);

        if (shipment == null)
        {
            shipment = new Shipment
            {
                Id = message.ShipmentId,
                OrderId = message.OrderId,
                ShipmentStatus = shipmentStatus,
                Message = message.Message!
            };
            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync();
        }

        _logger.LogInformation($"Processing shipment {shipment.ShipmentStatus}");
        await Task.Delay(3000);
        shipment.ShipmentStatus = Domain.Enums.ShipmentStatus.Shipped;

        await _context.SaveChangesAsync();

        await _publishEndpoint.Publish(new OrderStatusUpdatedIntegrationEvent(shipment.OrderId, OrderStatus.Completed));
        _logger.LogInformation($"Order {message.OrderId} has been shipped and completed.");
    }
}