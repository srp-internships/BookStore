using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Aplication.Interfaces
{
    public interface ICartService
    {
        Task<Cart> GetCartAsync(Guid userId);
        Task AddCartAsync(Cart cart);
        Task AddOrUpdateCartItemAsync(CartItem cartItem);
        Task UpdateItemQuantityAsync(Guid cartItemId, int quantity);
        Task RemoveItemFromCartAsync(Guid cartItemId);
        Task ClearCartAsync(Guid cartId);
    }
}
