using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Create;
using ShipmentService.Domain.Entities;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Update;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Delete;
using ShipmentService.Aplication.CQRS.Shipments.Queries.GetById;
using ShipmentService.Aplication.CQRS.Shipments.Queries.GetAll;

namespace ShipmentService.Aplication.Common.Mappings
{
    public class ShipmentMappings: Profile
    {
        public ShipmentMappings() 
        {
            CreateMap<Shipment, CreateShipmentCommand>()
                    .ReverseMap();
            CreateMap<Shipment, UpdateShipmentCommand>()
                .ReverseMap();
            CreateMap<Shipment, DeleteShipmentCommand>()
                .ForMember(dest => dest.ShipmentId, opt => opt.MapFrom(src => src.ShipmentId))
                .ReverseMap();
            CreateMap<Shipment, GetShipmentByIdQuery>()
                .ReverseMap();
            CreateMap<Shipment, GetShipmentsQuery>()
                .ReverseMap();
        }
    }
}
