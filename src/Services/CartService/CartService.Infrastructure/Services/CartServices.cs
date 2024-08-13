using CartService.Aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Infrastructure.Services
{
    public class CartServices : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartServices(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<Cart> GetCartAsync(Guid userId)
        {
            return await _cartRepository.GetCartAsync(userId);
        }

        public async Task AddCartAsync(Cart cart)
        {
            await _cartRepository.AddCartAsync(cart);
        }

        public async Task AddOrUpdateCartItemAsync(CartItem cartItem)
        {
            await _cartRepository.AddOrUpdateCartItemAsync(cartItem);
        }

        public async Task UpdateItemQuantityAsync(Guid cartItemId, int quantity)
        {
            await _cartRepository.UpdateItemQuantityAsync(cartItemId, quantity);
        }

        public async Task RemoveItemFromCartAsync(Guid cartItemId)
        {
            await _cartRepository.RemoveItemFromCartAsync(cartItemId);
        }

        public async Task ClearCartAsync(Guid cartId)
        {
            await _cartRepository.ClearCartAsync(cartId);
        }
    }
}