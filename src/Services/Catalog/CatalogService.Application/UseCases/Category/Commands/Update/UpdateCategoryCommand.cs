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
    public class UpdateCategoryCommand : IRequest, IMapWith<Category>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCategoryCommand, Category>();
        }
    }
}
