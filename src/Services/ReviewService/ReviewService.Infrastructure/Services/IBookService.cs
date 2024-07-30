using CatalogService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Infrastructure.Services
{
    public interface IBookService
    {
        Task AddBookAsync(BookCreatedEvent bookCreatedEvent);
        Task<bool> BookExistsAsync(Guid bookId);
    }
}
