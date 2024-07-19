namespace OrderServiceIntegrationEvents.EventHandlers;

public class OrderProcessedEventHandler
    (IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<OrderProcessedEventHandler> logger)
    : INotificationHandler<OrderProcessedEvent>
{
    public async Task Handle(OrderProcessedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        if (await featureManager.IsEnabledAsync("OrderFullfilment"))
        {
            var orderProcessedIntegrationEvent = domainEvent.order.ToOrderDto();
            await publishEndpoint.Publish(orderProcessedIntegrationEvent, cancellationToken);
        }
    }
}
