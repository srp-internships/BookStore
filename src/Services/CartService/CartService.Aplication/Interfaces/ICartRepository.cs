namespace CartService.Aplication.Interfaces
{
    public interface ICartRepository
    {
       
        Task<Cart> GetCartAsync(Guid userId);
        Task AddCartAsync(Cart cart);
        Task AddOrUpdateCartItemAsync(CartItem cartItem);
        Task<CartItem> GetCartItemAsync(Guid cartItemId);
        Task RemoveCartItemAsync(Guid cartItemId);
        Task SaveChangesAsync();
    }
}

