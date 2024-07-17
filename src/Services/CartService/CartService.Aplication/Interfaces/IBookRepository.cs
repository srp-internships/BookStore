using CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Aplication.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(Guid bookId);
        Task UpdateAsync(Book book);
    }
}
