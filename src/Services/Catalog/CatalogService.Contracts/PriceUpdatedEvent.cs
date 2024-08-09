namespace CatalogService.Contracts
{
    public class PriceUpdatedEvent
    {
        public Guid BookId { get; set; }
        public Guid SellerId { get; set; }
        public decimal Price { get; set; }
    }
}
