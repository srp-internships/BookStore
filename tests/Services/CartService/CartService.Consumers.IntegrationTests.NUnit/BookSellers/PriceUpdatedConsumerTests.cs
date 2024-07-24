using CartService.Consumers.BookSellers;
using CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Consumers.IntegrationTests.NUnit.BookSellers
{
    public class PriceUpdatedConsumerTests
    {
        private CartDbContext _context;
        private Mock<ILogger<PriceUpdatedConsumer>> _loggerMock;
        private PriceUpdatedConsumer _consumer;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CartDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new CartDbContext(options);
            _loggerMock = new Mock<ILogger<PriceUpdatedConsumer>>();
            _consumer = new PriceUpdatedConsumer(_loggerMock.Object, _context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Consume_PriceUpdatedEvent_ShouldUpdatePriceInDatabase()
        {
            // Arrange
            var existingBookSeller = new BookSeller
            {
                BookId = Guid.NewGuid(),
                SellerId = Guid.NewGuid(),
                Price = 10.0m
            };

            _context.BookSellers.Add(existingBookSeller);
            await _context.SaveChangesAsync();

            var existingCartItem = new CartItem
            {
                Id = Guid.NewGuid(),
                BookId = existingBookSeller.BookId,
                CartId = Guid.NewGuid(), // Assuming CartId is a valid GUID
                BookName = "Test Book",
                ImageUrl = "TestImage.png",
                Price = 10.0m,
                Quantity = 1,
                SellerId = existingBookSeller.SellerId
            };

            _context.Items.Add(existingCartItem);
            await _context.SaveChangesAsync();

            var priceUpdatedEvent = new PriceUpdatedEvent
            {
                BookId = existingBookSeller.BookId,
                SellerId = existingBookSeller.SellerId,
                Price = 20.0m
            };

            var consumeContextMock = new Mock<ConsumeContext<PriceUpdatedEvent>>();
            consumeContextMock.Setup(x => x.Message).Returns(priceUpdatedEvent);

            // Act
            await _consumer.Consume(consumeContextMock.Object);

            // Assert
            var updatedPrice = await _context.BookSellers
                .FirstOrDefaultAsync(p => p.BookId == priceUpdatedEvent.BookId &&
                                          p.SellerId == priceUpdatedEvent.SellerId);
            Assert.That(updatedPrice, Is.Not.Null);
            Assert.That(updatedPrice.Price, Is.EqualTo(priceUpdatedEvent.Price));

            var updatedCartItem = await _context.Items
                .FirstOrDefaultAsync(ci => ci.BookId == priceUpdatedEvent.BookId &&
                                            ci.SellerId == priceUpdatedEvent.SellerId);
            Assert.That(updatedCartItem, Is.Not.Null);
            Assert.That(updatedCartItem.Price, Is.EqualTo(priceUpdatedEvent.Price));
        }
    }
}

