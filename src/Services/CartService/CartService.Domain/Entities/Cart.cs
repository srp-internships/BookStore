using System.ComponentModel.DataAnnotations;

namespace CartService.Domain.Entities
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<CartItem>? cartItems { get; set; } = new List<CartItem>();
    }
}
