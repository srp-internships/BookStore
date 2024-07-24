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

            var book = await _context.Books.FirstOrDefaultAsync(
                b => b.Id == context.Message.Id);
            if (book != null)
            {
                book.Title = context.Message.Title;
                book.Image= context.Message.Image;
                _context.Books.Update(book);
            }

            var cartItems = await _context.Items
                .Where(ci => ci.BookId == context.Message.Id)
                .ToListAsync();

            foreach (var item in cartItems)
            {
                item.BookName = context.Message.Title;
                item.ImageUrl = context.Message.Image;
            }

            _context.Items.UpdateRange(cartItems);
            await _context.SaveChangesAsync();
        }
    }
}
