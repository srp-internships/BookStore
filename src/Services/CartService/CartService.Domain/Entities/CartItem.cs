using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CartService.Domain.Entities
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Book")]
        public Guid BookId { get; set; }
        [ForeignKey("Cart")]
        public Guid CartId { get; set; }
        public string? BookName { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid SellerId { get; set; }
    }   
}
