using CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Aplication.Interfaces
{
    public interface ICartServices
    {
        Task AddToCartAsync(Guid bookId, int quantity, Guid sellerId);
        Task<List<Cart>> GetCartItemsAsync(Guid userId);
        Task<decimal> GetCartTotalAsync(Guid userId);
        Task UpdateCartItemAsync(Guid cartItemId, int quantity);
        Task RemoveFromCartAsync(Guid cartItemId);
    }
}
