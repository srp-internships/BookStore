using CartService.Consumers.Books;
using CartService.Domain.Entities;

namespace CartService.Consumers.IntegrationTests.NUnit
{
    public class BookUpdatedConsumerTests
    {
        private CartDbContext _context;
        private Mock<ILogger<BookUpdatedConsumer>> _loggerMock;
        private BookUpdatedConsumer _consumer;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CartDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new CartDbContext(options);

            _loggerMock = new Mock<ILogger<BookUpdatedConsumer>>();
            _consumer = new BookUpdatedConsumer(_loggerMock.Object, _context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Consume_BookUpdatedEvent_ShouldUpdateBookInDatabase()
        {
            // Arrange
            var existingBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Original Title",
                Image = "OriginalImage.png"
            };

            _context.Books.Add(existingBook);
            await _context.SaveChangesAsync();

            var bookUpdatedEvent = new BookUpdatedEvent
            {
                Id = existingBook.Id,
                Title = "Updated Title",
                Image = "UpdatedImage.png"
            };

            var consumeContextMock = new Mock<ConsumeContext<BookUpdatedEvent>>();
            consumeContextMock.Setup(x => x.Message).Returns(bookUpdatedEvent);

            // Act
            await _consumer.Consume(consumeContextMock.Object);

            // Assert
            var book = await _context.Books.FindAsync(bookUpdatedEvent.Id);
            Assert.That(book, Is.Not.Null);
            Assert.That(book.Title, Is.EqualTo(bookUpdatedEvent.Title));
            Assert.That(book.Image, Is.EqualTo(bookUpdatedEvent.Image));
        }
    }
}
