using MassTransit;
using ShipmentService.IntegrationEvent;

namespace OrderService.IntegrationTests.Shipments;

public class ShipmentUpdatedEventPublisher
{
    private readonly IBusControl _busControl;

    public ShipmentUpdatedEventPublisher(IBusControl busControl)
    {
        _busControl = busControl;
    }

    public async Task PublishShipmentUpdatedEvent(ShipmentUpdatedEvent evenMessage)
    {
        await _busControl.Publish(evenMessage);
    }
}
