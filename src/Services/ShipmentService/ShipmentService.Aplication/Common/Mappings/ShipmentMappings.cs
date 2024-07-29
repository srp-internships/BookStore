using AutoMapper;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Create;
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
            CreateMap<Shipment, CreateShipmentCommand>()
                    .ReverseMap();
            CreateMap<UpdateShipmentCommand, Shipment>()
            .ForMember(dest => dest.ShipmentId, opt => opt.MapFrom(src => src.ShipmentId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.UpdateShipmentStatus, opt => opt.MapFrom(src => src.UpdatedStatusDateTime));
            CreateMap<Shipment, GetShipmentByIdQuery>()
                .ReverseMap();
            CreateMap<Shipment, GetShipmentsQuery>()
                .ReverseMap();
        }
    }
}
