using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Entities
{
    public class BookSeller
    {
        public Guid Id { get; set; }
        public Book? Book { get; set; }
        public Guid BookId { get; set; }
        public Seller? Seller { get; set; }
        public Guid SellerId { get; set; }
        public required decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
