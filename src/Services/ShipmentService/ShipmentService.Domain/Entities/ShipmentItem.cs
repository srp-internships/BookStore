using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Domain.Entities
{
    public class ShipmentItem
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
