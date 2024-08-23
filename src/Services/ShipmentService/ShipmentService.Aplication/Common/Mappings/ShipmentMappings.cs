using AutoMapper;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Update;
using ShipmentService.Aplication.CQRS.Shipments.Queries.GetAll;
using ShipmentService.Aplication.CQRS.Shipments.Queries.GetById;
using ShipmentService.Domain.Entities.Shipments;

namespace ShipmentService.Aplication.Common.Mappings
{
    public class ShipmentMappings: Profile
    {
        public ShipmentMappings() 
        {
            CreateMap<UpdateShipmentCommand, Shipment>();
            CreateMap<Shipment, GetShipmentByIdQuery>()
                .ReverseMap();
            CreateMap<Shipment, GetShipmentsQuery>()
                .ReverseMap();
        }
    }
}
