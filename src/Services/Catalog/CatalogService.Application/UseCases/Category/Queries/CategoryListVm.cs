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
    public class CategoryListVm : IMapWith<CategoryDto>
    {
        public IList<CategoryDto> Categories { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryDto>();
        }
    }
}
