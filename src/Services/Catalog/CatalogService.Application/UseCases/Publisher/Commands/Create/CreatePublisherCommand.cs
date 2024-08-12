using AutoMapper;
using CatalogService.Application.Mappers;
using CatalogService.Application.Models;
using CatalogService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class CreatePublisherCommand : IRequest<Guid>, IMapWith<Publisher>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public FileRequest Logo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePublisherCommand, Publisher>()
                .ForMember(dest => dest.Logo, opt => opt.Ignore());
        }
    }
}
