using CartService.Consumers.BookSellers;

namespace CartService.Consumers.IntegrationTests.NUnit.BookSellers
{
    public class PriceCreatedConsumerTests
    {
        private  CartDbContext? _context;
        private Mock<ILogger<PriceCreatedConsumer>> _loggerMock;
        private PriceCreatedConsumer _consumer;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CartDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new CartDbContext(options);

            _loggerMock = new Mock<ILogger<PriceCreatedConsumer>>();
            _consumer = new PriceCreatedConsumer(_loggerMock.Object, _context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Consume_PriceCreatedEvent_ShouldAddBookToDatabase()
        {
            // Arrange
            var priceCreatedEvent = new PriceCreatedEvent
            {
                BookId = Guid.NewGuid(),
                SellerId = Guid.NewGuid(),
                Price = 20
            };

            var consumeContextMock = new Mock<ConsumeContext<PriceCreatedEvent>>();
            consumeContextMock.Setup(x => x.Message).Returns(priceCreatedEvent);

            // Act
            await _consumer.Consume(consumeContextMock.Object);

            // Assert
            var price = await _context.BookSellers.FindAsync(priceCreatedEvent.BookId,priceCreatedEvent.SellerId);
            Assert.That(price,Is.Not.Null);
            Assert.That(price.Price, Is.EqualTo(priceCreatedEvent.Price));
        }
    }
}

