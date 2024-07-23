using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Interfaces
{
    public interface IBookSellerRepository
    {
        Task<Guid> CreateAsync(BookSeller bookSeller, CancellationToken token = default);
        Task<BookSeller> GetByIdAsync(Guid id, CancellationToken token = default);
        Task UpdateAsync(BookSeller bookSeller, CancellationToken token = default);
        Task DeleteAsync(Guid id, CancellationToken token = default);
    }
}
