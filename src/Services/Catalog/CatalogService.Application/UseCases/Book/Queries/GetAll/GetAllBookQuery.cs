
using CatalogService.Application.UseCases.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class GetAllBookQuery : IRequest<List<BookDto>>
    {

    }
}
