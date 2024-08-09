namespace PaymentService.IntegrationEvents
{
    public sealed record PaymentRequestProcessedEvent(
        Guid EventId,
        DateTime OccurredOnUtc,
        Guid OrderId,
        PaymentStatus PaymentStatus,
        PaymentMessage? Message);
}
