using AutoMapper;
using CatalogService.Application.Dto;

using CatalogService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{ 
    public class GetAllCategoryQuery : IRequest<CategoryListVm>
    {

    }
}
