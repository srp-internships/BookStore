namespace ReviewService.Domain.Repositories
{
    public interface IBookRepository
    {
        Task AddBookAsync(Book book);
        Task<bool> BookExistsAsync(Guid bookId);
    }
}
