using CartService.Aplication.Interfaces;
using CartService.Domain.Entities;
using CartService.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly CartDbContext _context;

        public CartRepository(CartDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartByUserIdAsync(Guid userId)
        {
            return await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task AddItemToCartAsync(Guid userId, CartItem item)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
            }

            cart.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartItemAsync(Guid userId, CartItem item)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart == null) return;

            var cartItem = cart.Items.FirstOrDefault(i => i.Id == item.Id);
            if (cartItem != null)
            {
                cartItem.BookId = item.BookId;
                cartItem.BookName = item.BookName;
                cartItem.ImageUrl = item.ImageUrl;
                cartItem.Price = item.Price;
                cartItem.Quantity = item.Quantity;
                cartItem.SellerId = item.SellerId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveItemFromCartAsync(Guid userId, Guid itemId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart == null) return;

            var cartItem = cart.Items.FirstOrDefault(i => i.Id == itemId);
            if (cartItem != null)
            {
                cart.Items.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<decimal> GetCartTotalAsync(Guid userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart == null) return 0;

            return cart.Items.Sum(i => i.Price * i.Quantity);
        }
    }
}
