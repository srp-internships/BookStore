namespace ShipmentService.Web.Models
{
    public class ShipmentDto
    {
        public Guid ShipmentId { get; set; }
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public ShippingAddressDto ShippingAddress { get; set; }
        public List<ItemDto> Items { get; set; }
        public int Status { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
    }
}
