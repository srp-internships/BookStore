using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Aplication.Commons.DTOs
{
    public class AddOrUpdateCartItemDto
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
    }
}
