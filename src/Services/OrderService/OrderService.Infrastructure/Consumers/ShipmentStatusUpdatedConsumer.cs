using MassTransit;
using Microsoft.Extensions.Logging;
using OrderService.Application.Common.Extensions;
using OrderService.Application.Common.Interfaces.Data;
using OrderService.Domain.Entities;
using OrderService.IntegrationEvents;
using ShipmentService.IntegrationEvent;

namespace OrderService.Infrastructure.Consumers;

public class ShipmentStatusUpdatedConsumer : IConsumer<ShipmentUpdatedEvent>
{
    private readonly ILogger<ShipmentStatusUpdatedConsumer> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDistributedCache _distributedCache;

    public ShipmentStatusUpdatedConsumer(ILogger<ShipmentStatusUpdatedConsumer> logger,
        ApplicationDbContext context,
        IPublishEndpoint publishEndpoint,
        IUnitOfWork unitOfWork,
        IDistributedCache distributedCache)
    {
        _logger = logger;
        _context = context;
        _publishEndpoint = publishEndpoint;
        _unitOfWork = unitOfWork;
        _distributedCache = distributedCache;
    }

    public async Task Consume(ConsumeContext<ShipmentUpdatedEvent> context)
    {
        var message = context.Message;

        var shipmentStatus = message.Status.ToDomainShipmentStatus();

        var shipment = await _context.Shipments.FindAsync(message.OrderId);

        if (shipment == null)
        {
            shipment = new Shipment
            {
                Id = message.ShipmentId,
                OrderId = message.OrderId,
                ShipmentStatus = message.Status.ToDomainShipmentStatus(),
                Message = message.Message!
            };

            _context.Shipments.Add(shipment);
        }
        else
        {
            shipment.Id = message.ShipmentId;
            shipment.ShipmentStatus = message.Status.ToDomainShipmentStatus();
            shipment.Message = message.Message;
        }

        await _unitOfWork.SaveChangesAsync();

        var order = await _context.Orders.FindAsync(message.OrderId);

        if (order != null)
        {
            if (shipmentStatus == Domain.Enums.ShipmentStatus.Shipped)
            {
                order.Status = Domain.Enums.OrderStatus.ShipmentProcessing;
                await _unitOfWork.SaveChangesAsync();

                await _publishEndpoint.Publish(new OrderStatusUpdatedIntegrationEvent(shipment.OrderId, OrderStatus.ShipmentProcessing));
                _logger.LogInformation($"Order {message.OrderId} shipped and processing to be delivered.");
            }
            else if (shipmentStatus == Domain.Enums.ShipmentStatus.Delivered)
            {
                order.Status = Domain.Enums.OrderStatus.Completed;
                await _unitOfWork.SaveChangesAsync();

                await _publishEndpoint.Publish(new OrderStatusUpdatedIntegrationEvent(shipment.OrderId, OrderStatus.Completed));
                _logger.LogInformation($"Order {message.OrderId} has been delivered and successfully completed.");
            }
            else
            {
                _logger.LogInformation($"Order {message.OrderId} shipment status is pending.");
            }

            var cacheKey = $"CustomerOrders_{order.CustomerId}";
            await _distributedCache.RemoveAsync(cacheKey);
        }
        else
        {
            _logger.LogWarning($"Order {message.OrderId} not found.");
        }
    }
}