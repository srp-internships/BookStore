using Microsoft.EntityFrameworkCore;
using ShipmentService.Aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipmentService.Infrastructure.Persistence.DbContexts;
using ShipmentService.Domain.Entities.Shipments;

namespace ShipmentService.Infrastructure.Repositories
{
    public class ShipmentRepository: IShipmentRepository
    {
        private readonly ShipmentContext _context;

        public ShipmentRepository(ShipmentContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shipment>> GetAllShipmentsAsync()
        {
            return await _context.Shipments
                .Include(s => s.Items)
                .Include(s => s.ShippingAddress)
                .ToListAsync();
        }

        public async Task<Shipment?> GetShipmentByIdAsync(Guid shipmentId)
        {
            return await _context.Shipments
                .Include(s => s.Items)
                .Include(s => s.ShippingAddress)
                .FirstOrDefaultAsync(s => s.ShipmentId == shipmentId);
        }

        public async Task UpdateShipmentAsync(Shipment shipment)
        {
            var existingShipment = await _context.Shipments
                .Include(s => s.Items)
                .Include(s => s.ShippingAddress)
                .FirstOrDefaultAsync(s => s.ShipmentId == shipment.ShipmentId);

            if (existingShipment != null)
            {
                _context.Entry(existingShipment).CurrentValues.SetValues(shipment);
                foreach (var item in shipment.Items)
                {
                    var existingItem = existingShipment.Items
                        .FirstOrDefault(i => i.ItemId == item.ItemId);
                    if (existingItem != null)
                    {
                        _context.Entry(existingItem).CurrentValues.SetValues(item);
                    }
                    else
                    {
                        existingShipment.Items.Add(item);
                    }
                }

                var existingAddress = existingShipment.ShippingAddress;
                if (existingAddress != null)
                {
                    _context.Entry(existingAddress).CurrentValues.SetValues(shipment.ShippingAddress);
                }
                else
                {
                    existingShipment.ShippingAddress = shipment.ShippingAddress;
                }
            }
            else
            {
                _context.Shipments.Add(shipment);
            }

            await _context.SaveChangesAsync();
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
