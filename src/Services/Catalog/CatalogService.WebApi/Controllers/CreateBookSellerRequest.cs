namespace CatalogService.WebApi.Controllers
{
    public class CreateBookSellerRequest
    {
        public Guid BookId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
