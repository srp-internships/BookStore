namespace CartService.Aplication.Commons.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdAsync(Guid userId);
        Task<CartItem?> GetCartItemByIdAsync(Guid cartItemId);
        Task<CartItem?> GetCartItemByBookIdAsync(Guid cartId, Guid bookId);
        Task AddCartAsync(Cart cart);
        Task AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task DeleteCartAsync(Guid cartId);
        Task DeleteCartItemAsync(Guid cartItemId);
    }
}

