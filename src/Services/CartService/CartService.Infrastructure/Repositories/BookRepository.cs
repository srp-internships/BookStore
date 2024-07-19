using CartService.Aplication.Interfaces;
using CartService.Infrastructure.Persistence.Contexts;

namespace CartService.Infrastructure.Repositories
{
    public class BookRepository: IBookRepository
    {
        private readonly CartDbContext _dbContext; 

        public BookRepository(CartDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book> GetByIdAsync(Guid bookId)
        {
            return await _dbContext.Set<Book>().FindAsync(bookId);
        }

        public async Task UpdateAsync(Book book)
        {
            _dbContext.Entry(book).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
