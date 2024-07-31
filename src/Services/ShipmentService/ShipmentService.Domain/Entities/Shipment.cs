using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Domain.Entities
{
    public class Shipment
    {
        public Guid ShipmentId { get; set; }
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public ShippingAddress? ShippingAddress { get; set; }
        public List<ShipmentItem>? Items { get; set; }
        public Status Status { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }= DateTime.Now;      
        public Shipment() { }


    }
}
