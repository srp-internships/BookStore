using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<Guid> CreateAsync(Book book, CancellationToken token = default);
        Task<Book?> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<List<Book>> GetAllAsync(CancellationToken token = default);
        Task<List<Book>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken token = default);
        Task UpdateAsync(Book book, CancellationToken token = default);
        Task UpdateAuthorsAsync(Guid id, IEnumerable<Author> newAuthors, CancellationToken token = default);
        Task UpdateCategoriesAsync(Guid id, IEnumerable<Category> newCategories, CancellationToken token = default);
        Task UpdatePublisherAsync(Guid id, Guid newPublisherId, CancellationToken token = default);
        Task DeleteAsync(Guid id, CancellationToken token = default);
    }
}
