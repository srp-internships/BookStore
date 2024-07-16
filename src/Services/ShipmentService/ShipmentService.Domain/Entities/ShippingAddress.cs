using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Domain.Entities
{
    public class ShippingAddress
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
