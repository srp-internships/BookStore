using ShipmentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.IntegrationEvent
{
    public class ShipmentRequest
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public ShippingAddress? ShippingAddress { get; set; }
        public List<ShipmentItem>? Items { get; set; }
    }
}
