using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Create;
using ShipmentService.Domain.Entities;

namespace ShipmentService.Aplication.Common.Mappings
{
    public class ShipmentMappings: Profile
    {
        public ShipmentMappings() 
        {
            CreateMap<CreateShipmentCommand, Shipment>();
        }
    }
}
