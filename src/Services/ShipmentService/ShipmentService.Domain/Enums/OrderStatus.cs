using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Domain.Enums
{
    public enum OrderStatus
    {
        Completed,
        Failed,
        PaymentProcessing,
        ShipmentProcessing
    }
}
