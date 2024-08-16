using MassTransit;
using Microsoft.Extensions.Logging;
using OrderService.IntegrationEvents;
using ShipmentService.Aplication.Common.Extentions;
using ShipmentService.Domain.Entities.Shipments;
using ShipmentService.Domain.Enums;
using ShipmentService.Infrastructure.Persistence.DbContexts;

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
                var orderEvent = context.Message;

                var shipment = new Shipment
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
                    Status = Status.Draft,
                    OrderStatus = OrderStatusConverter.ToShipmentOrderStatus(orderEvent.Status),
                    Items = orderEvent.Items.Select(item => new ShipmentItem
                    {
                        Id = Guid.NewGuid(),
                        BookId = item.BookId,
                        BookName = item.Title,
                        Quantity = item.Quantity
                    }).ToList()
                };

                _context.Shipments.Add(shipment);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Created shipment for OrderId {orderEvent.OrderId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process OrderProcessedIntegrationEvent");
            }
        }
    }
}

