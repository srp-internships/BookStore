using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts
{
    public class PriceUpdatedEvent
    {
        public Guid BookId { get; set; }
        public Guid SellerId { get; set; }
        public decimal Price { get; set; }
    }
}
