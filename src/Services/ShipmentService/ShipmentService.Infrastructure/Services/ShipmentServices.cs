using ShipmentService.Aplication.Interfaces;
using ShipmentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Infrastructure.Services
{
    public class ShipmentServices: IShipmentService
    {
        private readonly IShipmentRepository _shipmentRepository;

        public ShipmentServices(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public Task<Shipment> GetShipmentByIdAsync(Guid shipmentId)
        {
            return _shipmentRepository.GetShipmentByIdAsync(shipmentId);
        }

        public Task CreateShipmentAsync(Shipment shipment)
        {
            return _shipmentRepository.CreateShipmentAsync(shipment);
        }

        public Task UpdateShipmentAsync(Shipment shipment)
        {
            return _shipmentRepository.UpdateShipmentAsync(shipment);
        }

        public Task DeleteShipmentAsync(Guid shipmentId)
        {
            return _shipmentRepository.DeleteShipmentAsync(shipmentId);
        }
    }
}

