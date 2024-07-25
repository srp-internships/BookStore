namespace ShipmentService.Web.Models
{
    public class ItemDto
    {
        public Guid ItemId { get; set; }
        public string BookName { get; set; }
        public int Quantity { get; set; }
    }
}
