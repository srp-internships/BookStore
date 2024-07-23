using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<Guid> CreateAsync(Book book, CancellationToken token = default);
        Task<Book> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<IEnumerable<Book>> GetAllAsync(CancellationToken token = default);
        Task<IEnumerable<Book>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken token = default);
        Task UpdateAsync(Book book, CancellationToken token = default);
        Task DeleteAsync(Guid id, CancellationToken token = default);
    }
}
