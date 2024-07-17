using CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Aplication.Interfaces
{
    public interface IBookService
    {
        Task<Book> GetBookAsync(Guid bookId);
        Task UpdateBookAsync(Book book);
    }
}
