using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Domain.Entities.Shipments
{
    public class ShipmentItem
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public string? BookName { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
