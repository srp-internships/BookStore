using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class UpdateAmountBookSellerCommand : IRequest
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
    }
}
