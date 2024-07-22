using System.ComponentModel.DataAnnotations;

namespace CartService.Domain.Entities
{
    public class Cart
    {
        [Key]
        public Guid Id  { get; set; } 
        public Guid UserId { get; set; }
        public List<CartItem>? Items { get; set; } = new List<CartItem>();
        public decimal TotalPrice
        {
            get
            {
                return Items.Sum(item => item.Price * item.Quantity);
            }
        }
    }
}
