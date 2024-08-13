using AutoMapper;
using CatalogService.Application.Mappers;
using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class PublisherDto : IMapWith<PublisherDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Publisher, PublisherDto>();
        }
    }
}
