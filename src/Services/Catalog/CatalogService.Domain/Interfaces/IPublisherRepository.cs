using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Interfaces
{
    public interface IPublisherRepository
    {
        Task<Guid> CreateAsync(Publisher publisher, CancellationToken token = default);
        Task<Publisher> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<IEnumerable<Publisher>> GetAllAsync(CancellationToken token = default);
        Task UpdateAsync(Publisher publisher, CancellationToken token = default);
        Task DeleteAsync(Guid id, CancellationToken token = default);
    }
}

