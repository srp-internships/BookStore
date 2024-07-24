using Microsoft.EntityFrameworkCore;

namespace CartService.Consumers.Books
{
    public class BookUpdatedConsumer : IConsumer<BookUpdatedEvent>
    {
        private readonly ILogger<BookUpdatedConsumer> _logger;
        private readonly CartDbContext _context;

        public BookUpdatedConsumer(ILogger<BookUpdatedConsumer> logger, CartDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Consume(ConsumeContext<BookUpdatedEvent> context)
        {
            _logger.LogInformation("Book Updated: {Id}, {Title}, {Image}",
                context.Message.Id, context.Message.Title, context.Message.Image);

            var cartItems = await _context.Items
                .Where(ci => ci.BookId == context.Message.Id)
                .ToListAsync();

            foreach (var item in cartItems)
            {
                item.BookName = context.Message.Title;
            }

            _context.Items.UpdateRange(cartItems);
            await _context.SaveChangesAsync();
        }
    }
}
