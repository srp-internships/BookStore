namespace OrderService.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItem> Items { get; set; }
    public Payment PaymentId { get; set; }
    public Shipment ShipmentId { get; set; }
    public Customer CustomerId { get; set; }
    public Cart CartId { get; set; }
}
