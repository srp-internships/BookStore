using FluentAssertions;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using OrderService.IntegrationEvents;
using PaymentService.IntegrationEvents;
using Xunit;

namespace OrderService.IntegrationTests.Payments;

public class PaymentRequestProcessedEventConsumerTests
{
    private readonly ServiceProvider DI;
    private readonly ITestHarness _harness;

    public PaymentRequestProcessedEventConsumerTests()
    {
        var services = ExtensionMethods.CreateServiceProvider();
        services.RegisterInMemoryDbContext();
        services.RegisterMassTransit();
        DI = services.BuildServiceProvider();
        _harness = DI.GetRequiredService<ITestHarness>();
    }

    [Fact]
    public async Task Consumer_ShouldRaiseOrderStatusUpdatedIntegrationEventToFailed()
    {
        // Arrange
        await _harness.Start();

        var order = await DI.AddOrder();

        var publisher = DI.GetRequiredService<PaymentRequestProcessedEventPublisher>();

        var testEvent = new PaymentRequestProcessedEvent(
            Guid.NewGuid(),
            DateTime.UtcNow,
            order.Id,
            PaymentStatus.Failed,
            new PaymentMessage("Info", "Payment failed.")
            );

        // Act
        await publisher.PublishPaymentRequestProcessedEvent(testEvent);

        // Assert
        (await _harness.Published.Any<OrderStatusUpdatedIntegrationEvent>()).Should().BeTrue();
        (await _harness.Published.Any<PaymentRequestProcessedEvent>()).Should().BeTrue();
        var publishedEvent = (await _harness.Published.SelectAsync<OrderStatusUpdatedIntegrationEvent>().First()).Context.Message;
        publishedEvent.OrderId.Should().Be(order.Id);
        publishedEvent.Status.Should().Be(OrderStatus.Failed);
        await _harness.Stop();
    }

    [Fact]
    public async Task Consumer_ShouldRaiseOrderStatusUpdatedIntegrationEventToShipmentProcessing()
    {
        // Arrange
        await _harness.Start();
        var order = await DI.AddOrder();

        var publisher = DI.GetRequiredService<PaymentRequestProcessedEventPublisher>();

        var testEvent = new PaymentRequestProcessedEvent(
            Guid.NewGuid(),
            DateTime.UtcNow,
            order.Id,
            PaymentStatus.Succeeded,
            new PaymentMessage("Info", "Payment processed successfully.")
        );

        // Act
        await publisher.PublishPaymentRequestProcessedEvent(testEvent);

        // Assert
        (await _harness.Published.Any<OrderStatusUpdatedIntegrationEvent>()).Should().BeTrue();
        (await _harness.Published.Any<PaymentRequestProcessedEvent>()).Should().BeTrue();
        var publishedEvent = (await _harness.Published.SelectAsync<OrderStatusUpdatedIntegrationEvent>().First()).Context.Message;
        publishedEvent.OrderId.Should().Be(order.Id);
        publishedEvent.Status.Should().Be(OrderStatus.ShipmentProcessing);
        await _harness.Stop();
    }
}
