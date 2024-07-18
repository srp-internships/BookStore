namespace CartService.Domain.Entities
{
    public class UpdateQuantityRequest
    {
        public Guid BookId { get; set; }
        public int NewQuantity { get; set; }
    }
}
