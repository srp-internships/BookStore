using CatalogService.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ReviewService.Domain.Entities;
using ReviewService.Infrastructure.Persistence.Contexts;
using System.Threading.Tasks;
using Xunit;

namespace ReviewService.Infrastructure.Consumers.IntegrationsTests.xUnit
{
    public class BookCreatedConsumerTests
    {

        private readonly BookCreatedConsumer _consumer;
        private readonly Mock<ILogger<BookCreatedConsumer>> _loggerMock;
        private readonly ReviewDbContext _context;

        public BookCreatedConsumerTests()
        {
            // Set up in-memory database
            var dbContextOptions = new DbContextOptionsBuilder<ReviewDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _context = new ReviewDbContext(dbContextOptions);
            _loggerMock = new Mock<ILogger<BookCreatedConsumer>>();
            _consumer = new BookCreatedConsumer(_loggerMock.Object, _context);
        }

        [Fact]
        public async Task Consume_ShouldAddBook_WhenBookDoesNotExist()
        {
            // Arrange
            var bookCreatedEvent = new BookCreatedEvent { Id = Guid.NewGuid() };
            var context = new Mock<ConsumeContext<BookCreatedEvent>>();
            context.Setup(c => c.Message).Returns(bookCreatedEvent);

            // Act
            await _consumer.Consume(context.Object);

            // Assert
            var book = await _context.Books.FindAsync(bookCreatedEvent.Id);
            Assert.NotNull(book);
            Assert.Equal(bookCreatedEvent.Id, book.Id);
        }

        [Fact]
        public async Task Consume_ShouldNotAddBook_WhenBookAlreadyExists()
        {
            // Arrange
            var existingBookId = Guid.NewGuid();
            _context.Books.Add(new Book { Id = existingBookId });
            await _context.SaveChangesAsync();

            var bookCreatedEvent = new BookCreatedEvent { Id = existingBookId };
            var context = new Mock<ConsumeContext<BookCreatedEvent>>();
            context.Setup(c => c.Message).Returns(bookCreatedEvent);

            // Act
            await _consumer.Consume(context.Object);

            // Assert
            var booksCount = await _context.Books.CountAsync(b => b.Id == existingBookId);
            Assert.Equal(1, booksCount); // Ensure no duplicate entries
        }
        [Fact]
        public async Task Consume_BookCreatedEvent_ShouldAddBookToDatabase()
        {
            // Arrange
            var bookCreatedEvent = new BookCreatedEvent { Id = Guid.NewGuid() };
            var context = new Mock<ConsumeContext<BookCreatedEvent>>();
            context.Setup(c => c.Message).Returns(bookCreatedEvent);

            // Act
            await _consumer.Consume(context.Object);

            // Assert
            var book = await _context.Books.FindAsync(bookCreatedEvent.Id);
            Assert.NotNull(book);
            Assert.Equal(bookCreatedEvent.Id, book.Id);
        }

    }
}
