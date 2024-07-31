namespace OrderService.Application.Mappers;

public static class PaymentStatusMapper
{
    public static OrderService.Domain.Enums.PaymentStatus ToDomainPaymentStatus(PaymentService.IntegrationEvents.PaymentStatus status)
    {
        return status switch
        {
            PaymentService.IntegrationEvents.PaymentStatus.Pending => OrderService.Domain.Enums.PaymentStatus.Pending,
            PaymentService.IntegrationEvents.PaymentStatus.Succeeded => OrderService.Domain.Enums.PaymentStatus.Succeeded,
            PaymentService.IntegrationEvents.PaymentStatus.Failed => OrderService.Domain.Enums.PaymentStatus.Failed,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}
