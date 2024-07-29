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
                _logger.LogInformation("Order Created: {OrderId}, {CustomerId}",
                    context.Message.OrderId, context.Message.CustomerId);

                var shipment = CreateShipmentFromOrder(context.Message);

                _context.Shipments.Add(shipment);
                await _context.SaveChangesAsync();
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
                    Id=Guid.NewGuid(),
                    Street = orderEvent.ShippingAddress.Street,
                    City = orderEvent.ShippingAddress.State,
                    Country = orderEvent.ShippingAddress.Country
                },
                Items = orderEvent.OrderItems.Select(item => new ShipmentItem
                {
                    ItemId = item.BookId,
                    BookName = item.Title,
                    Quantity = item.Quantity
                }).ToList(),
                Status = Status.Pending
                
            };
        }
    }
}
