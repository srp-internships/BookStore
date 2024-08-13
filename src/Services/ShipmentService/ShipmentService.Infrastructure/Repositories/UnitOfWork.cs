using ShipmentService.Aplication.Interfaces;
using ShipmentService.Infrastructure.Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Infrastructure.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ShipmentContext _context;

        public UnitOfWork(ShipmentContext context, IShipmentRepository shipmentRepository)
        {
            _context = context;
            Shipments = shipmentRepository;
        }

        public IShipmentRepository Shipments { get; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
