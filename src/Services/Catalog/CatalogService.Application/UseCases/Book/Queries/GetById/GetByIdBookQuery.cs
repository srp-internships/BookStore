using CatalogService.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class GetByIdBookQuery : IRequest<BookDto>
    {
        public Guid Id { get; set; }        
    }
}
