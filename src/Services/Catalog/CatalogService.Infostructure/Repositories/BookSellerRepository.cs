
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CatalogService.Infostructure.Repositories
{
    public class BookSellerRepository
        (CatalogDbContext dbContext) : IBookSellerRepository
    {
        private readonly CatalogDbContext _dbcontext = dbContext;

        public async Task<Guid> CreateAsync(BookSeller bookSeller, CancellationToken token = default)
        {
            await _dbcontext.BookSellers.AddAsync(bookSeller, token);
            return bookSeller.Id;
        }
        public Task<BookSeller?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return _dbcontext.BookSellers
                .Include(bs => bs.Book)
                .Include(bs => bs.Seller)
                .FirstOrDefaultAsync(bs => bs.Id.Equals(id), token);
        }
        public Task<List<BookSeller>> GetListByBookIdAsync(Guid id, CancellationToken token = default)
        {
            return _dbcontext.BookSellers
                .Include(bs => bs.Book)
                .Include(bs => bs.Seller)
                .Where(p => p.BookId.Equals(id))
                .ToListAsync(token);
        }
        public async Task UpdateAsync(BookSeller bookSeller, CancellationToken token = default)
        {
            await _dbcontext.BookSellers
                .Where(u => u.Id.Equals(bookSeller.Id))
                .ExecuteUpdateAsync(update => update
                    .SetProperty(u => u.Price, bookSeller.Price)
                    .SetProperty(u => u.Description, bookSeller.Description), token);
        }

        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            await _dbcontext.BookSellers.Where(p => p.Id.Equals(id)).ExecuteDeleteAsync(token);
        }

        public Task<BookSeller?> GetByTwinId(Guid bookId, Guid sellerId, CancellationToken token = default)
        {
            return _dbcontext.BookSellers.FirstOrDefaultAsync(i => i.BookId == bookId && i.SellerId == sellerId, token);

        }
        public Task<bool> AnyAsync(Guid id, CancellationToken token = default)
        {
            return _dbcontext.BookSellers.AnyAsync(prop => prop.Id.Equals(id));
        }
    }
}
