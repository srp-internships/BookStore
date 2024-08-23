using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<Guid> CreateAsync(Category category, CancellationToken token = default);
        Task<Category?> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<List<Category>> GetAllAsync(CancellationToken token = default);
        Task<List<Category>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken token = default);
        Task UpdateAsync(Category category, CancellationToken token = default);
        Task DeleteAsync(Guid id, CancellationToken token = default);
        Task<bool> AnyAsync(Guid id, CancellationToken token = default);
    }
}
