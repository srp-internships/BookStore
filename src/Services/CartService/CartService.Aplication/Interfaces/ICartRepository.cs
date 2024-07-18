using CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Aplication.Interfaces
{
    public interface ICartRepository
    {
        //Task<Cart> GetCartByUserIdAsync(Guid userId);
        //Task AddItemToCartAsync(Guid userId, CartItem item);
        //Task UpdateCartItemAsync(Guid userId, CartItem item);
        //Task RemoveItemFromCartAsync(Guid userId, Guid itemId);
        //Task<decimal> GetCartTotalAsync(Guid userId);

        Task<Cart> GetCartAsync(Guid userId);
        Task AddCartAsync(Cart cart);
        Task AddOrUpdateCartItemAsync(CartItem cartItem);
        Task<CartItem> GetCartItemAsync(Guid cartItemId);
        Task RemoveCartItemAsync(Guid cartItemId);
        Task SaveChangesAsync();
    }
}

