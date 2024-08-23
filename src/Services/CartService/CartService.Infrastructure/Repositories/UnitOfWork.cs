using CartService.Aplication.Commons.Interfaces;
using CartService.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CartDbContext _context;
        public UnitOfWork(CartDbContext context,
                          ICartRepository cartRepository,
                          IBookRepository books,
                          IBookSellerRepositoty bookSeller)
        {
            _context = context;
            Carts = cartRepository;
            Books = books;
            BookSellers= bookSeller;
        }
        public IBookSellerRepositoty BookSellers { get; }
        public ICartRepository Carts { get; }
        public IBookRepository Books { get; }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
