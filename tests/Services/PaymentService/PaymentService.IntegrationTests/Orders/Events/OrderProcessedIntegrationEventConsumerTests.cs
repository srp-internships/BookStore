using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using OrderService.IntegrationEvents;
using PaymentService.Application.Orders;
using PaymentService.IntegrationEvents;

namespace PaymentService.IntegrationTests.Orders.Events
{
    public class OrderProcessedIntegrationEventConsumerTests
    {
        private readonly ServiceProvider DI;
        private readonly ITestHarness _harness;

        public OrderProcessedIntegrationEventConsumerTests()
        {
            var services = ExtensionMethods.CreateServiceProvider();
            services.RegisterInMemoryDbContext();
            services.RegisterMassTransit();
            DI = services.BuildServiceProvider();
            _harness = DI.GetRequiredService<ITestHarness>();
        }

        [Fact]
        public async Task Consumer_Should_RisePaymentSuccessEvent()
        {
            // Arrange
            await _harness.Start();
            var customer = await DI.AddCustomer();
            var seller = await DI.AddSeller();

            var publisher = DI.GetRequiredService<OrderTestIntegrationEventPublisher>();

            var testEvent = new OrderProcessedIntegrationEvent(Guid.NewGuid(), customer.UserId,
                new Address("", "", "", "", "", ""),
                OrderStatus.PaymentProcessing,
                [
                    new(Guid.NewGuid(), "", seller.UserId, 5, 3m),
                ],
                55);

            // Act
            await publisher.PublishOrderProcessedEvent(testEvent);

            // Assert
            (await _harness.Published.Any<OrderProcessedIntegrationEvent>()).Should().BeTrue();
            (await _harness.Published.Any<PaymentRequestProcessedEvent>()).Should().BeTrue();
            var publishedEvent = (await _harness.Published.SelectAsync<PaymentRequestProcessedEvent>().First()).Context.Message;
            publishedEvent.OrderId.Should().Be(testEvent.OrderId);
            publishedEvent.PaymentStatus.Should().Be(PaymentStatus.Succeeded);
            await _harness.Stop();
        }

        [Fact]
        public async Task Consumer_Should_RisePaymentFailureEvent_WhenCustomerDoesNotHaveCard()
        {
            // Arrange
            await _harness.Start();
            var seller = await DI.AddSeller();

            var publisher = DI.GetRequiredService<OrderTestIntegrationEventPublisher>();

            var testEvent = new OrderProcessedIntegrationEvent(Guid.NewGuid(), Guid.NewGuid(),
                new Address("", "", "", "", "", ""),
                OrderStatus.PaymentProcessing,
                [
                    new(Guid.NewGuid(), "", seller.UserId, 5, 3),
                ],
                55);

            // Act
            await publisher.PublishOrderProcessedEvent(testEvent);

            // Assert
            (await _harness.Published.Any<OrderProcessedIntegrationEvent>()).Should().BeTrue();
            (await _harness.Published.Any<PaymentRequestProcessedEvent>()).Should().BeTrue();
            var publishedEvent = (await _harness.Published.SelectAsync<PaymentRequestProcessedEvent>().First()).Context.Message;
            publishedEvent.OrderId.Should().Be(testEvent.OrderId);
            publishedEvent.PaymentStatus.Should().Be(PaymentStatus.Failed);
            publishedEvent.Message.Key.Should().Be(OrderErrors.CustomerDoesNotHaveCard().Code);
            await _harness.Stop();
        }

        [Fact]
        public async Task Consumer_Should_RisePaymentFailureEvent_WhenSellerDoesNotHaveCard()
        {
            // Arrange
            await _harness.Start();
            var customer = await DI.AddCustomer();

            var publisher = DI.GetRequiredService<OrderTestIntegrationEventPublisher>();

            var testEvent = new OrderProcessedIntegrationEvent(Guid.NewGuid(), customer.UserId,
                new Address("", "", "", "", "", ""),
                OrderStatus.PaymentProcessing,
                [
                    new(Guid.NewGuid(), "", Guid.NewGuid(), 5, 3),
                ],
                55);

            // Act
            await publisher.PublishOrderProcessedEvent(testEvent);

            // Assert
            (await _harness.Published.Any<OrderProcessedIntegrationEvent>()).Should().BeTrue();
            (await _harness.Published.Any<PaymentRequestProcessedEvent>()).Should().BeTrue();
            var publishedEvent = (await _harness.Published.SelectAsync<PaymentRequestProcessedEvent>().First()).Context.Message;
            publishedEvent.OrderId.Should().Be(testEvent.OrderId);
            publishedEvent.PaymentStatus.Should().Be(PaymentStatus.Failed);
            publishedEvent.Message.Key.Should().Be(OrderErrors.SellerDoesNotHaveCard().Code);
            await _harness.Stop();
        }
    }
}
