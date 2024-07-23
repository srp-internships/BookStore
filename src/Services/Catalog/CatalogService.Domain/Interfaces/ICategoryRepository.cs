using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Guid> CreateAsync(Category category, CancellationToken token = default);
        Task<Category> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<IEnumerable<Category>> GetAllAsync(CancellationToken token = default);
        Task<IEnumerable<Category>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken token = default);
        Task UpdateAsync(Category category, CancellationToken token = default);
        Task DeleteAsync(Guid id, CancellationToken token = default);
    }
}
