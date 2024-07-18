namespace CartService.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid CartId { get; set; }
        public string? BookName { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid SellerId { get; set; }

    }   
}
