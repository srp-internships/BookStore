using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Aplication.Commons.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICartRepository Carts { get; }
        IBookRepository Books { get; }
        IBookSellerRepositoty BookSellers { get; }
        Task<int> CompleteAsync();
    }
}
