using System.ComponentModel.DataAnnotations;

namespace ShipmentService.IntegrationEvent
{
    public class ShipmentProcces
    {
        [Key]
        public Guid ShipmentId { get; set; }
        public ShipmentStatus Stats { get; set; }
        public DateTime estimatedDeliveryDate { get; set; }
        public string Message { get; set; }

    }
}
