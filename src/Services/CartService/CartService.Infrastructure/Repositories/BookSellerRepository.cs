using CartService.Aplication.Commons.Interfaces;
using CartService.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Infrastructure.Repositories
{
    public class BookSellerRepository: IBookSellerRepositoty
    {
        private readonly CartDbContext _context;

        public BookSellerRepository(CartDbContext context)
        {
            _context = context;
        }
        public async Task<BookSeller?> GetSellerByBookIdAsync(Guid bookId)
        {
            return await _context.BookSellers
                .FirstOrDefaultAsync(bs => bs.BookId == bookId);
        }
        public async Task<decimal> GetPriceByBookIdAndSellerIdAsync(Guid bookId, Guid sellerId)
        {
            var bookSeller = await _context.BookSellers
                .FirstOrDefaultAsync(bs => bs.BookId == bookId && bs.SellerId == sellerId);
            return bookSeller?.Price ?? 0m;
        }
    }
}
