using AutoMapper;
using CatalogService.Application.Mappers;
using CatalogService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class UpdatePublisherCommand : IRequest, IMapWith<Publisher>
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePublisherCommand, Publisher>();
        }
    }
}
