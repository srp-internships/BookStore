using ShipmentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShipmentService.Aplication.Interfaces
{
    public interface IShipmentService
    {
        Task<Shipment> GetShipmentByIdAsync(Guid shipmentId);
        Task CreateShipmentAsync(Shipment shipment);
        Task UpdateShipmentAsync(Shipment shipment);
        Task DeleteShipmentAsync(Guid shipmentId);
    }
}
