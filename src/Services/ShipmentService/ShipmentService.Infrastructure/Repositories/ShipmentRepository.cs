using Microsoft.EntityFrameworkCore;
using ShipmentService.Domain.Entities;
using ShipmentService.Aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipmentService.Infrastructure.Persistence.DbContexts;

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

        public async Task AddShipmentAsync(Shipment shipment)
        {
            await _context.Shipments.AddAsync(shipment);
        }

        public async Task UpdateShipmentAsync(Shipment shipment)
        {
            _context.Shipments.Update(shipment);
        }

        public async Task DeleteShipmentAsync(Guid shipmentId)
        {
            var shipment = await GetShipmentByIdAsync(shipmentId);
            if (shipment != null)
            {
                _context.Shipments.Remove(shipment);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
