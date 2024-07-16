using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.IntegrationEvent
{
    public class ShipmentUpdated
    {
        [Key]
        public Guid ShipmentId { get; set; }
        public Guid OrderId { get; set; }
        public ShipmentStatus Status { get; set; }
        public DateTime estimatedDeliveryDate { get; set; }
        public string? currentLocation { get; set; }
        public string? Message { get; set; }
    }
}
