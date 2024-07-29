namespace OrderService.Domain.Entities;

public class Shipment : BaseEntity
{
    public string ShipmentStatus { get; set; }
    public string Message { get; set; }
}
