namespace OrderService.Domain.Entities;

public class OrderItem : BaseEntity
{
    public int Quantity { get;  set; }
    public decimal Price { get;  set; }

    public Order Order {  get; set; }   
    public Guid OrderId { get; set; } 
    public Book Book { get; set; }
    public Guid BookId { get;  set; }
   
}
