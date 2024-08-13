using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Interfaces.Repositories
{
    public interface IBookSellerRepository
    {
        Task CreateAsync(BookSeller bookSeller, CancellationToken token = default);
        Task<BookSeller> GetByIdAsync(Guid id, CancellationToken token = default);
        Task UpdateAsync(BookSeller bookSeller, CancellationToken token = default);
        Task DeleteAsync(Guid id, CancellationToken token = default);
        Task<List<BookSeller>> GetByTwinId(Guid bookId, Guid sellerId, CancellationToken token = default);
        Task<bool> AnyAsync(Guid id, CancellationToken token = default);
    }
}
