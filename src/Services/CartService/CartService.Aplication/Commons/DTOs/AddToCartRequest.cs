using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Aplication.Commons.DTOs
{
    public class AddToCartRequest
    {
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
        public Guid SellerId { get; set; }
    }
}
