using ShipmentService.Domain.Enums;
using ShipmentService.IntegrationEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.Common.Extentions
{
    public static class DomainIntegrationEnumsConverter
    {
        public static ShipmentStatus ToIntegrationEnum(this ShipmentService.Domain.Enums.Status status)
        {
            return status switch
            {
                ShipmentService.Domain.Enums.Status.Pending => ShipmentStatus.Pending,
                ShipmentService.Domain.Enums.Status.Shipped => ShipmentStatus.Shipped,
                ShipmentService.Domain.Enums.Status.Delivered => ShipmentStatus.Delivered,
                _ => throw new InvalidCastException($"Not matchable domain enum value detected: {status}"),
            };
        }

        public static ShipmentService.Domain.Enums.Status ToDomainEnum(this ShipmentStatus status)
        {
            return status switch
            {
                ShipmentStatus.Pending => ShipmentService.Domain.Enums.Status.Pending,
                ShipmentStatus.Shipped => ShipmentService.Domain.Enums.Status.Shipped,
                ShipmentStatus.Delivered => ShipmentService.Domain.Enums.Status.Delivered,
                _ => throw new InvalidCastException($"Not matchable domain enum value detected: {status}"),
            };
        }
    }
}
