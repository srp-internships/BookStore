using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.IntegrationEvents;

public sealed record OrderShipmentRequestEvent(
    Guid OrderId,
    Guid CustomerId,
    ShippingAddress Address,
    OrderItem Item
    );
