using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Aplication.Commons.Interfaces
{
    public interface IBookSellerRepositoty
    {
        Task<BookSeller?> GetSellerByBookIdAsync(Guid bookId);
        Task<decimal> GetPriceByBookIdAndSellerIdAsync(Guid bookId, BookSeller sellerId);
    }
}
