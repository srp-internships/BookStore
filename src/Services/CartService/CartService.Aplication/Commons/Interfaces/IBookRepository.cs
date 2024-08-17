namespace CartService.Aplication.Commons.Interfaces
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(Guid bookId);
        Task<bool> IsAvailableAsync(Guid bookId);
    }

}
