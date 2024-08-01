using ShipmentService.Domain.Entities.Shipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.Interfaces
{
    public interface IShipmentRepository
    {
        Task<IEnumerable<Shipment>> GetAllShipmentsAsync();
        Task<Shipment?> GetShipmentByIdAsync(Guid shipmentId);
        Task UpdateShipmentAsync(Shipment shipment);
        Task SaveChangesAsync();
    }
}
