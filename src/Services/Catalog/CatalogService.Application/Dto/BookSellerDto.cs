using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Dto
{
    public class BookSellerDto
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid SellerId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
