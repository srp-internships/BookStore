using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Aplication.Commons.Interfaces
{
    public interface ICartService
    {
        Task<Cart?> GetCartByUserIdAsync(Guid userId);
        Task CreateCartAsync(Cart cart);
        Task AddToCartAsync(Guid userId, CartItem cartItem);
        Task UpdateCartItemQuantityAsync(Guid cartItemId, int quantity);
        Task RemoveCartItemAsync(Guid cartItemId);
        Task ClearCartAsync(Guid userId);
        Task<decimal> GetTotalPriceAsync(Guid userId);
        Task<bool> IsBookAvailableAsync(Guid bookId);
    }

}
