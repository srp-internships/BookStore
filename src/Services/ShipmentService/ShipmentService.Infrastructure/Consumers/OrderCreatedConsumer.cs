using MassTransit;
using Microsoft.Extensions.Logging;
using OrderService.IntegrationEvents;
using ShipmentService.Domain.Entities;
using ShipmentService.Domain.Entities.Shipments;
using ShipmentService.Domain.Enums;
using ShipmentService.Infrastructure.Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Infrastructure.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderProcessedIntegrationEvent>
    {
        private readonly ILogger<OrderCreatedConsumer> _logger;
        private readonly ShipmentContext _context;

        public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger, ShipmentContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Consume(ConsumeContext<OrderProcessedIntegrationEvent> context)
        {
            try
            {
                var message = context.Message;

                _logger.LogInformation("Order Created: {OrderId}, {CustomerId}",
                    message.OrderId, message.CustomerId);

                var shipment = CreateShipmentFromOrder(message);

                _context.Shipments.Add(shipment);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Shipment created and saved: {ShipmentId}", shipment.ShipmentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing OrderCreatedEvent for OrderId: {OrderId}",
                    context.Message.OrderId);
            }
        }

        private Shipment CreateShipmentFromOrder(OrderProcessedIntegrationEvent orderEvent)
        {
            return new Shipment
            {
                ShipmentId = Guid.NewGuid(),
                OrderId = orderEvent.OrderId,
                CustomerId = orderEvent.CustomerId,
                ShippingAddress = new ShippingAddress
                {
                    Id = Guid.NewGuid(),
                    Street = orderEvent.ShippingAddress.Street,
                    City = orderEvent.ShippingAddress.State,
                    Country = orderEvent.ShippingAddress.Country
                },
                Items = orderEvent.OrderItems.Select(item => new ShipmentItem
                {
                    ItemId = item.BookId,
                    BookName = item.Title,
                    Quantity = item.Quantity,
                }).ToList(),
                Status = Status.Shipped
            };
        }
    }
}
