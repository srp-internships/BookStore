namespace CartService.Aplication.Interfaces
{
    public interface IBookService
    {
        Task<Book> GetBookAsync(Guid bookId);
        Task UpdateBookAsync(Book book);
    }
}
