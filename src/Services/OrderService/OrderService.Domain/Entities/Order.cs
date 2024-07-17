namespace OrderService.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItem> Items { get; set; }
    public Payment Payment { get; set; }
    public Guid PaymentId { get; set; }
    public Shipment Shipment { get; set; }
    public Guid ShipmentId { get; set; }
    public Customer Customer { get; set; }
    public Guid CustomerId { get; set; }
    public Cart Cart { get; set; }
    public Guid CartId { get; set; }
}
