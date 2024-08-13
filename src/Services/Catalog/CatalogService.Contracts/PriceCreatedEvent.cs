namespace CatalogService.Contracts
{
    public class PriceCreatedEvent
    {
        public Guid BookId { get; set; }
        public Guid SellerId { get; set; }
        public decimal Price { get; set; }
    }
}