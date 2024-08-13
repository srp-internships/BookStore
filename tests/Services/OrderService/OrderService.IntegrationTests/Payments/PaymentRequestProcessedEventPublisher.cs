using MassTransit;
using PaymentService.IntegrationEvents;

namespace OrderService.IntegrationTests.Payments;

public class PaymentRequestProcessedEventPublisher
{
    private readonly IBusControl _busControl;

    public PaymentRequestProcessedEventPublisher(IBusControl busControl)
    {
        _busControl = busControl;
    }

    public async Task PublishPaymentRequestProcessedEvent(PaymentRequestProcessedEvent eventMessage)
    {
        await _busControl.Publish(eventMessage);
    }
}


