using AutoMapper;
using CatalogService.Application.Mappers;
using CatalogService.Application.UseCases.Queries;
using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class BookListVm
    {
        public List<BookDto> Books { get; set; }
        
    }
}
