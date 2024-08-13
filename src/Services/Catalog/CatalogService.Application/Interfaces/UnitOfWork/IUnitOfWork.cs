using CatalogService.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository Authors { get; }
        IBookRepository Books { get; }
        ICategoryRepository Categories { get; }
        IBookSellerRepository BookSellers { get; }
        IPublisherRepository Publishers { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
