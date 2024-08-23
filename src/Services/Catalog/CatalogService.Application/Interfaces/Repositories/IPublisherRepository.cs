using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces.Repositories
{
    public interface IPublisherRepository
    {
        Task<Guid> CreateAsync(Publisher publisher, CancellationToken token = default);
        Task<Publisher?> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<List<Publisher>> GetAllAsync(CancellationToken token = default);
        Task UpdateAsync(Publisher publisher, CancellationToken token = default);
        Task DeleteAsync(Guid id, CancellationToken token = default);
    }
}

