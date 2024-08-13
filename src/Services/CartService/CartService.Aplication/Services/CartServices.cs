using CartService.Aplication.Commons.DTOs;
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
            return await _unitOfWork.Carts.GetCartByUserIdAsync(userId);
        }

        public async Task AddToCartAsync(Guid userId, AddToCartRequest request)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                // Create a new cart if it does not exist
                cart = new Cart { Id = Guid.NewGuid(), UserId = userId };
                await _unitOfWork.Carts.AddCartAsync(cart);
                await _unitOfWork.CompleteAsync();
            }

            var book = await _unitOfWork.Books.GetByIdAsync(request.BookId);
            if (book == null)
            {
                throw new BookNotFoundException("Book not found.");
            }

            var bookSeller = await _unitOfWork.BookSellers.GetPriceByBookIdAndSellerIdAsync(request.BookId, request.SellerId);
            if (bookSeller == null)
            {
                throw new SellerNotFoundException("Book seller not found.");
            }

            var cartItem = new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                BookId = request.BookId,
                BookName = book.Title,
                ImageUrl = book.Image,
                Quantity = request.Quantity,
                Price = bookSeller.Price,
                SellerId = request.SellerId
            };

            var existingItem = cart.Items.FirstOrDefault(item => item.BookId == request.BookId && item.SellerId == request.SellerId);
            if (existingItem == null)
            {
                cart.Items.Add(cartItem);
            }
            else
            {
                existingItem.Quantity += request.Quantity;
                existingItem.Price = bookSeller.Price; 
            }

            await _unitOfWork.Carts.AddCartItemAsync(cartItem);
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
            var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                throw new CartNotFoundException("Cart not found.");
            }

            await _unitOfWork.Carts.DeleteCartAsync(cart.Id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<decimal> GetTotalPriceAsync(Guid userId)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                throw new CartNotFoundException("Cart not found.");
            }

            return cart.TotalPrice;
        }

        public async Task<bool> IsBookAvailableAsync(Guid bookId)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(bookId);
            return book != null;
        }
    }

}
