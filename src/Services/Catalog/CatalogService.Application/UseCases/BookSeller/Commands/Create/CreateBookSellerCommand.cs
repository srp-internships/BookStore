using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class CreateBookSellerCommand : IRequest<Guid>
    {
        public Guid SellerId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
