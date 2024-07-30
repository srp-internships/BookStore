using MassTransit;
using OrderService.IntegrationEvents;

namespace PaymentService.IntegrationTests.Orders.Events
{
    public class OrderTestIntegrationEventPublisher
    {
        private readonly IBusControl _busControl;

        public OrderTestIntegrationEventPublisher(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task PublishOrderProcessedEvent(OrderProcessedIntegrationEvent eventMessage)
        {
            await _busControl.Publish(eventMessage);
        }
    }
}