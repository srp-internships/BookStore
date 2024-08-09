namespace OrderService.Domain.Entities;

public class Shipment : BaseEntity
{
    public Order? Order { get; set; }
    public Guid OrderId { get; set; }
    public ShipmentStatus ShipmentStatus { get; set; }
    public string? Message { get; set; }
}
