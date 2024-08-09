namespace OrderService.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string? Message { get; set; }
    }
}
