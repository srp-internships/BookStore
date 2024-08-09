namespace OrderService.Application.Common.Extensions;

public static class PaymentStatusMapperExtensions
{
    public static PaymentStatus ToDomainPaymentStatus(this PaymentService.IntegrationEvents.PaymentStatus status)
    {
        return status switch
        {
            PaymentService.IntegrationEvents.PaymentStatus.Pending => PaymentStatus.Pending,
            PaymentService.IntegrationEvents.PaymentStatus.Succeeded => PaymentStatus.Succeeded,
            PaymentService.IntegrationEvents.PaymentStatus.Failed => PaymentStatus.Failed,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}
