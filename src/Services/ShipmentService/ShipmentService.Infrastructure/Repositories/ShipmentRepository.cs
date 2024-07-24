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

        public async Task<Shipment> GetShipmentByIdAsync(Guid shipmentId)
        {
            return await _context.Shipments
                .Include(s => s.ShippingAddress)
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.ShipmentId == shipmentId);
        }

        public async Task CreateShipmentAsync(Shipment shipment)
        {
            await _context.Shipments.AddAsync(shipment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateShipmentAsync(Shipment shipment)
        {
            _context.Shipments.Update(shipment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteShipmentAsync(Guid shipmentId)
        {
            var shipment = await GetShipmentByIdAsync(shipmentId);
            if (shipment != null)
            {
                _context.Shipments.Remove(shipment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
