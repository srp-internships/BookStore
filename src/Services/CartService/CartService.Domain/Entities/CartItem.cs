using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public string? BookName { get; set; }
        public string? ImageUrl {  get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid SellerId { get; set; }
    }   
}
