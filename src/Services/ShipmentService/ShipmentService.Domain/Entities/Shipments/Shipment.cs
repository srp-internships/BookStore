using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipmentService.Domain.Enums;

namespace ShipmentService.Domain.Entities.Shipments
{
    public class Shipment
    {
        [Key]
        public Guid ShipmentId { get; set; }
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public ShippingAddress? ShippingAddress { get; set; }
        public List<ShipmentItem>? Items { get; set; }
        public Status Status { get; set; }
        public DateTime UpdateShipmentStatus { get; set; } = DateTime.Now;
        public OrderStatus OrderStatus { get; set; }
        public Shipment()
        {
            Items = new List<ShipmentItem>();
        }
    }
}
