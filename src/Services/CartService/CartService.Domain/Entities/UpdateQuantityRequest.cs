    namespace CartService.Domain.Entities
{
    public class UpdateQuantityRequest
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public int NewQuantity { get; set; }
    }
}
