using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using ShipmentService.Infrastructure.Persistence.DbContexts;
using ShipmentService.Domain.Entities;

namespace ShipmentService.IntegrationEvent
{
    public class ShipmentRequestConsumer : IConsumer<ShipmentRequest>
    {
        private readonly ShipmentContext _context;

        public ShipmentRequestConsumer(ShipmentContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<ShipmentRequest> context)
        {
            var request = context.Message;

            var shipment = new Shipment
            {
                ShipmentId = Guid.NewGuid(),
                OrderId = request.OrderId,
                CustomerId = request.CustomerId,
                ShippingAddress = new ShippingAddress
                {
                    Street = request.ShippingAddress.Street,
                    City = request.ShippingAddress.City,
                    Country = request.ShippingAddress.Country
                },
                Items = request.Items.Select(i => new ShipmentItem
                {
                    ItemId = i.ItemId,
                    Quantity = i.Quantity
                }).ToList(),
                Status = 0,
                EstimatedDeliveryDate = DateTime.UtcNow.AddDays(5)
            };

            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Shipment created: {shipment.ShipmentId}, Address: {shipment.ShippingAddress.Street}");
        }
    }

}