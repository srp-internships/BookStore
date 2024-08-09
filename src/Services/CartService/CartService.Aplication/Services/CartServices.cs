using CartService.Aplication.Commons.Exceptions;
using CartService.Aplication.Commons.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Aplication.Services
{
    public class CartServices : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Cart?> GetCartByUserIdAsync(Guid userId)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                throw new CartNotFoundException("Cart not found.");
            }
            return cart;
        }

        public async Task CreateCartAsync(Cart cart)
        {
            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart));
            }

            var existingCart = await _unitOfWork.Carts.GetCartByUserIdAsync(cart.UserId);
            if (existingCart != null)
            {
                throw new InvalidOperationException("Cart already exists for this user.");
            }

            await _unitOfWork.Carts.AddCartAsync(cart);
            await _unitOfWork.CompleteAsync();
        }

        public async Task AddToCartAsync(Guid userId, CartItem cartItem)
        {
            if (cartItem == null)
            {
                throw new ArgumentNullException(nameof(cartItem));
            }

            var cart = await GetCartByUserIdAsync(userId);
            var book = await _unitOfWork.Books.GetByIdAsync(cartItem.BookId);

            if (book == null)
            {
                throw new BookNotFoundException("Book not found.");
            }

            var sellerId = await _unitOfWork.BookSellers.GetSellerByBookIdAsync(cartItem.BookId);
            var existingCartItem = await _unitOfWork.Carts.GetCartItemByBookIdAsync(cart.Id, cartItem.BookId);

            if (existingCartItem == null)
            {
                cartItem.Id = Guid.NewGuid();
                cartItem.CartId = cart.Id;
                cartItem.Price = await _unitOfWork.BookSellers.GetPriceByBookIdAndSellerIdAsync(cartItem.BookId,sellerId);
                cartItem.SellerId = sellerId.SellerId;

                await _unitOfWork.Carts.AddCartItemAsync(cartItem);
            }
            else
            {
                if (cartItem.Quantity <= 0)
                {
                    throw new InvalidQuantityException("Quantity must be greater than zero.");
                }

                existingCartItem.Quantity = cartItem.Quantity;
                await _unitOfWork.Carts.UpdateCartItemAsync(existingCartItem);
            }

            await _unitOfWork.CompleteAsync();
        }
        public async Task UpdateCartItemQuantityAsync(Guid cartItemId, int quantity)
        {
            if (quantity <= 0)
            {
                throw new InvalidQuantityException("Quantity must be greater than zero.");
            }

            var cartItem = await _unitOfWork.Carts.GetCartItemByIdAsync(cartItemId);
            if (cartItem == null)
            {
                throw new CartItemNotFoundException("Cart item not found.");
            }

            cartItem.Quantity = quantity;
            await _unitOfWork.Carts.UpdateCartItemAsync(cartItem);
            await _unitOfWork.CompleteAsync();
        }

        public async Task RemoveCartItemAsync(Guid cartItemId)
        {
            var cartItem = await _unitOfWork.Carts.GetCartItemByIdAsync(cartItemId);
            if (cartItem == null)
            {
                throw new CartItemNotFoundException("Cart item not found.");
            }

            await _unitOfWork.Carts.DeleteCartItemAsync(cartItemId);
            await _unitOfWork.CompleteAsync();
        }

        public async Task ClearCartAsync(Guid userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                throw new CartNotFoundException("Cart not found.");
            }

            foreach (var item in cart.Items.ToList())
            {
                await _unitOfWork.Carts.DeleteCartItemAsync(item.Id);
            }

            await _unitOfWork.Carts.DeleteCartAsync(cart.Id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<decimal> GetTotalPriceAsync(Guid userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            return cart.TotalPrice;
        }

        public async Task<bool> IsBookAvailableAsync(Guid bookId)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(bookId);
            return book != null;
        }
    }
}
