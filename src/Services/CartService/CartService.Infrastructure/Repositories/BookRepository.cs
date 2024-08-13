using CartService.Aplication.Commons.Interfaces;
using CartService.Infrastructure.Persistence.Contexts;

namespace CartService.Infrastructure.Repositories
{
    public class BookRepository: IBookRepository
    {
        private readonly CartDbContext _context;

        public BookRepository(CartDbContext context)
        {
            _context = context;
        }

        public async Task<Book?> GetByIdAsync(Guid bookId)
        {
            return await _context.Books
                .FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task<bool> IsAvailableAsync(Guid bookId)
        {
            return await _context.Books
                .AnyAsync(b => b.Id == bookId);
        }
    }
}
