namespace CartService.Aplication.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(Guid bookId);
        Task UpdateAsync(Book book);
    }
}
