using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using OrderService.IntegrationEvents;
using ShipmentService.Infrastructure.Consumers;
using ShipmentService.Infrastructure.Persistence.DbContexts;

namespace ShipmentService.Infrastructure.IntegrationTests.xUnitTests.Consumers
{
    public class OrderCreatedConsumerTests
    {
        private readonly Mock<ILogger<OrderCreatedConsumer>> _mockLogger;
        private readonly ShipmentContext _context;
        private readonly OrderCreatedConsumer _consumer;

        public OrderCreatedConsumerTests()
        {
            var options = new DbContextOptionsBuilder<ShipmentContext>()
                .UseInMemoryDatabase("TestDatabase") // Используйте In-Memory базу данных для тестов
                .Options;
            _context = new ShipmentContext(options);

            _mockLogger = new Mock<ILogger<OrderCreatedConsumer>>();
            _consumer = new OrderCreatedConsumer(_mockLogger.Object, _context);
        }

        [Fact]
        public async Task Consume_ShouldSaveShipmentToDatabase_WhenOrderStatusIsShipmentProcessing()
        {
            // Arrange
            var orderEvent = new OrderProcessedIntegrationEvent(
                Guid.NewGuid(), // OrderId
                Guid.NewGuid(), // CustomerId
                new Address("John", "Doe", "john.doe@example.com", "Country", "State", "123 Test St"),
                OrderStatus.ShipmentProcessing,
                new List<OrderItem>
                {
                new OrderItem(
                    Guid.NewGuid(), // BookId
                    "Test Book", // Title
                    Guid.NewGuid(), // SellerId
                    1, // Quantity
                    20.00m // Price
                )
                },
                100.00m // TotalPrice
            );

            var consumeContext = Mock.Of<ConsumeContext<OrderProcessedIntegrationEvent>>(
                ctx => ctx.Message == orderEvent
            );

            // Act
            await _consumer.Consume(consumeContext);

            // Assert
            var shipment = await _context.Shipments
                .Include(s => s.ShippingAddress)
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.OrderId == orderEvent.OrderId);

            Assert.NotNull(shipment);
            Assert.Equal(orderEvent.CustomerId, shipment.CustomerId);
            Assert.NotNull(shipment.ShippingAddress);
            Assert.Equal(orderEvent.ShippingAddress.Street, shipment.ShippingAddress.Street);
            Assert.Equal(orderEvent.ShippingAddress.State, shipment.ShippingAddress.City);
            Assert.Equal(orderEvent.ShippingAddress.Country, shipment.ShippingAddress.Country);
            Assert.Single(shipment.Items);
            Assert.Equal(orderEvent.Items[0].BookId, shipment.Items[0].ItemId);
            Assert.Equal(orderEvent.Items[0].Title, shipment.Items[0].BookName);
            Assert.Equal(orderEvent.Items[0].Quantity, shipment.Items[0].Quantity);
        }
    }
}
