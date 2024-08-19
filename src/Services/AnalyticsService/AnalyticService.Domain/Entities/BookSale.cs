using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticService.Domain.Entities
{
    public class BookSale
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Title { get; set; }
        public Guid SellerId { get; set; }
    }
}
