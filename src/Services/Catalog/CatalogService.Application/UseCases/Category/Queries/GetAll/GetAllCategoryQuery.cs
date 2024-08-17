﻿using AutoMapper;
using CatalogService.Domain.Entities;
using CatalogService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{ 
    public class GetAllCategoryQuery : IRequest<List<CategoryDto>>
    {

    }
}
