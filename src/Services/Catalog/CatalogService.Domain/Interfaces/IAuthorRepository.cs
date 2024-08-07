using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Guid> CreateAsync(Author author, CancellationToken token = default);
        Task<Author> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<List<Author>> GetAllAsync(CancellationToken token = default);
        Task<List<Author>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken token = default);
        Task UpdateAsync(Author author, CancellationToken token = default);
        Task DeleteAsync(Guid id, CancellationToken token = default);
    }
}
