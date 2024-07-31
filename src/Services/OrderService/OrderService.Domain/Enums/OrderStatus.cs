namespace OrderService.Domain.Enums;

public enum OrderStatus
{
    PaymentProcessing = 1,
    ShipmentProcessing = 2,
    Completed = 3,
    Failed = 4,
}
