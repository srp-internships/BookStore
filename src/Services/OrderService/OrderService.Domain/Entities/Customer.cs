namespace OrderService.Domain.Entities;

public class Customer : BaseEntity
{
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    public virtual ICollection<Order> Orders { get; private set; }
}
