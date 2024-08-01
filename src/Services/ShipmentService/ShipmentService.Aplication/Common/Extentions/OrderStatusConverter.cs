using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.Common.Extentions
{
    public static class OrderStatusConverter
    {
        public static ShipmentService.Domain.Enums.OrderStatus ToShipmentOrderStatus(OrderService.IntegrationEvents.OrderStatus status)
        {
            return status switch
            {
                OrderService.IntegrationEvents.OrderStatus.Completed => ShipmentService.Domain.Enums.OrderStatus.Completed,
                OrderService.IntegrationEvents.OrderStatus.Failed => ShipmentService.Domain.Enums.OrderStatus.Failed,
                OrderService.IntegrationEvents.OrderStatus.PaymentProcessing => ShipmentService.Domain.Enums.OrderStatus.PaymentProcessing,
                OrderService.IntegrationEvents.OrderStatus.ShipmentProcessing => ShipmentService.Domain.Enums.OrderStatus.ShipmentProcessing,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }

        public static OrderService.IntegrationEvents.OrderStatus ToIntegrationOrderStatus(ShipmentService.Domain.Enums.OrderStatus status)
        {
            return status switch
            {
                ShipmentService.Domain.Enums.OrderStatus.Completed => OrderService.IntegrationEvents.OrderStatus.Completed,
                ShipmentService.Domain.Enums.OrderStatus.Failed => OrderService.IntegrationEvents.OrderStatus.Failed,
                ShipmentService.Domain.Enums.OrderStatus.PaymentProcessing => OrderService.IntegrationEvents.OrderStatus.PaymentProcessing,
                ShipmentService.Domain.Enums.OrderStatus.ShipmentProcessing => OrderService.IntegrationEvents.OrderStatus.ShipmentProcessing,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }

}
