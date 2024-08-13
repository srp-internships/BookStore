using CatalogService.Contracts;

namespace ReviewService.Application.Services
{
    public interface IBookService
    {
        Task AddBookAsync(BookCreatedEvent bookCreatedEvent);
        Task<bool> BookExistsAsync(Guid bookId);
    }
}
