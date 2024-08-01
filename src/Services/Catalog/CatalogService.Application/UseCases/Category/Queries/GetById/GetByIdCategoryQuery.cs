using AutoMapper;
using CatalogService.Application.Mappers;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class GetByIdCategoryQuery : IRequest<CategoryDto>
    { 
        public Guid Id { get; set; }
    }
}
