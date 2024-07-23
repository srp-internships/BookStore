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
        Task UpdateTitleAsync(Guid id, string title, CancellationToken token = default);
        Task UpdateImageAsync(Guid id, string image, CancellationToken token = default);
        Task UpdateAuthorsAsync(Guid id, IEnumerable<Author> newAuthors, CancellationToken token = default);
        Task UpdateCategoriesAsync(Guid id, IEnumerable<Category> newCategories, CancellationToken token = default);
        Task UpdatePublisherAsync(Guid id, Guid newPublisherId, CancellationToken token = default);
        Task DeleteAsync(Guid id, CancellationToken token = default);
    }
}
