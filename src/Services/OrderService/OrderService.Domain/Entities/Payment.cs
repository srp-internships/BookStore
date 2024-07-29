namespace OrderService.Domain.Entities;

public class Payment : BaseEntity
{
    public string? PaymentStatus { get; set; }
    public string? Message { get; set; }
}
