using CartService.Consumers.Books;

namespace CartService.Consumers.IntegrationTests.NUnit.Books
{
    public class BookCreatedConsumerTests
    {
        private CartDbContext _context;
        private Mock<ILogger<BookCreatedConsumer>> _loggerMock;
        private BookCreatedConsumer _consumer;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CartDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new CartDbContext(options);

            _loggerMock = new Mock<ILogger<BookCreatedConsumer>>();
            _consumer = new BookCreatedConsumer(_loggerMock.Object, _context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Consume_BookCreatedEvent_ShouldAddBookToDatabase()
        {
            // Arrange
            var bookCreatedEvent = new BookCreatedEvent
            {
                Id = Guid.NewGuid(),
                Title = "Test Book",
                Image = "TestImage.png"
            };

            var consumeContextMock = new Mock<ConsumeContext<BookCreatedEvent>>();
            consumeContextMock.Setup(x => x.Message).Returns(bookCreatedEvent);

            // Act
            await _consumer.Consume(consumeContextMock.Object);

            // Assert
            var book = await _context.Books.FindAsync(bookCreatedEvent.Id);
            Assert.That(book, Is.Not.Null);
            Assert.That(book.Title, Is.EqualTo(bookCreatedEvent.Title));
            Assert.That(book.Image, Is.EqualTo(bookCreatedEvent.Image));
        }
    }
}
