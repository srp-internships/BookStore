namespace OrderService.Domain.Entities;

public class Book : BaseEntity
{
    public string? Title { get; private set; }
    public Guid SellerId { get; private set; }
}