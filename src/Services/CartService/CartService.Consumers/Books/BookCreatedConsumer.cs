using CartService.Domain.Entities;

namespace CartService.Consumers.Books
{
    public class BookCreatedConsumer : IConsumer<BookCreatedEvent>
    {
        private readonly ILogger<BookCreatedConsumer> _logger;
        private readonly CartDbContext _context;

        public BookCreatedConsumer(ILogger<BookCreatedConsumer> logger, CartDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task Consume(ConsumeContext<BookCreatedEvent> context)
        {
            _logger.LogInformation("Book Created: {Id}, {Title}, {Image}",
                context.Message.Id, context.Message.Title, context.Message.Image);

            var book = new Book
            {
                Id = context.Message.Id,
                Title = context.Message.Title,
                Image = context.Message.Image

            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
    }
}
