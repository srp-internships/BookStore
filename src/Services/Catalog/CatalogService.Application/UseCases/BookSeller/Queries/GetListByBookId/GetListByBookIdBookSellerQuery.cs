using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class GetListByBookIdBookSellerQuery : IRequest<List<BookSellerDto>>
    {
        public Guid BookId { get; set; }
    }
}
