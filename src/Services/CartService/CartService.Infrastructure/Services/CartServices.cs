using CartService.Aplication.Interfaces;
using CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Infrastructure.Services
{
    public class CartServices: ICartServices
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICatalogService _catalogService;

        public CartServices(ICartRepository cartRepository, ICatalogService catalogService)
        {
            _cartRepository = cartRepository;
            _catalogService = catalogService;
        }

        public async Task AddToCartAsync(Guid bookId, int quantity, Guid sellerId)
        {
            var book = await _catalogService.GetBookAsync(bookId);
            if (book == null)
            {
                throw new InvalidOperationException("Книга не найдена в каталоге.");
            }

            var cartItem = new CartItem
            {
                BookId = bookId,
                Quantity = quantity,
                TotalPrice = book.Price * quantity,
                SellerId = sellerId
            };

            await _cartRepository.AddAsync(cartItem);
        }

        public async Task<List<Cart>> GetCartItemsAsync(Guid userId)
        {
            return await _cartRepository.GetCartItemsAsync(userId);
        }

        public async Task<decimal> GetCartTotalAsync(Guid userId)
        {
            var items = await _cartRepository.GetCartItemsAsync(userId);
            return items.Sum(item => item.TotalPrice);
        }

        public async Task UpdateCartItemAsync(Guid cartItemId, int quantity)
        {
            var item = await _cartRepository.GetCartItemByIdAsync(cartItemId);
            if (item != null)
            {
                item.Quantity = quantity;
                item.TotalPrice = item.Book.Price * quantity;
                await _cartRepository.UpdateAsync(item);
            }
        }

        public async Task RemoveFromCartAsync(Guid cartItemId)
        {
            await _cartRepository.RemoveAsync(cartItemId);
        }
    }
}
