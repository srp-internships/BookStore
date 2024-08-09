using ReviewService.Domain.Repositories;
using ReviewService.Infrastructure.Persistence.Contexts;

namespace ReviewService.Infrastructure.Repositories
{
    public class BookRepository: IBookRepository
    {
        private readonly ReviewDbContext _context;

        public BookRepository(ReviewDbContext context)
        {
            _context = context;
        }

        public async Task AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> BookExistsAsync(Guid bookId)
        {
            return await _context.Books.AnyAsync(b => b.Id == bookId);
        }
    }
}
