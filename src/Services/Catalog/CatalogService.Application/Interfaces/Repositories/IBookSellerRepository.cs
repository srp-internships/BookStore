using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces.Repositories
{
    public interface IBookSellerRepository
    {
        Task<Guid> CreateAsync(BookSeller bookSeller, CancellationToken token = default);
        Task<BookSeller?> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<List<BookSeller>?> GetListByBookIdAsync(Guid id, CancellationToken token = default);
        Task UpdateAsync(BookSeller bookSeller, CancellationToken token = default);
        Task DeleteAsync(Guid id, CancellationToken token = default);
        Task<BookSeller?> GetByTwinId(Guid bookId, Guid sellerId, CancellationToken token = default);
        Task<bool> AnyAsync(Guid id, CancellationToken token = default);
    }
}
