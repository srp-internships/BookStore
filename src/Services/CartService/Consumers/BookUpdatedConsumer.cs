using CartService.Domain.Entities;
using CartService.Infrastructure.Persistence.Contexts;
using MassTransit;

namespace Consumers
{
    public class BookUpdatedConsumer : IConsumer<Book>
    {
        private readonly CartDbContext _context;

        public BookUpdatedConsumer(CartDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<Book> context)
        {
            var book = context.Message;

            var existingBook = await _context.Books.FindAsync(book.BookId);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Price = book.Price;
                existingBook.Image = book.Image;
                existingBook.Quantity = book.Quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}
