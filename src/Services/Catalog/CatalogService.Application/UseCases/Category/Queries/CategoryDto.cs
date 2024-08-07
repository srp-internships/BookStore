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
    public class CategoryDto : IMapWith<CategoryDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryDto>();
        }
    }
}
