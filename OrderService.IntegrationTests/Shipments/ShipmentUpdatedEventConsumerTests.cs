using FluentAssertions;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using OrderService.IntegrationEvents;
using ShipmentService.IntegrationEvent;
using Xunit;

namespace OrderService.IntegrationTests.Shipments;

public class ShipmentUpdatedEventConsumerTests
{
    private readonly ServiceProvider DI;
    private readonly ITestHarness _testHarness;

    public ShipmentUpdatedEventConsumerTests()
    {
        var services = ExtensionMethods.CreateServiceProvider();
        services.RegisterInMemoryDbContext();
        services.RegisterMassTransit();
        DI = services.BuildServiceProvider();
        _testHarness = DI.GetRequiredService<ITestHarness>();
    }

    [Fact]
    public async Task Consumer_ShouldRaiseOrderStatusUpdatedIntegrationEventToCompleted()
    {
        // Arrange
        await _testHarness.Start();
        var order = await DI.AddOrder();

        var publisher = DI.GetRequiredService<ShipmentUpdatedEventPublisher>();

        var testEvent = new ShipmentUpdatedEvent(
            Guid.NewGuid(),
            order.Id,
            ShipmentStatus.Delivered,
            DateTime.UtcNow,
            "Shipment delivered successfully"
        );

        // Act 
        await publisher.PublishShipmentUpdatedEvent(testEvent);

        // Assert
        (await _testHarness.Published.Any<OrderStatusUpdatedIntegrationEvent>()).Should().BeTrue();
        (await _testHarness.Published.Any<ShipmentUpdatedEvent>()).Should().BeTrue();

        var publishedEvent = (await _testHarness.Published.SelectAsync<OrderStatusUpdatedIntegrationEvent>().First()).Context.Message;
        publishedEvent.OrderId.Should().Be(order.Id);
        publishedEvent.Status.Should().Be(OrderStatus.Completed);
    }

}
