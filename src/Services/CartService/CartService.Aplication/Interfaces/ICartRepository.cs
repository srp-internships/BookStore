namespace CartService.Aplication.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartAsync(Guid userId);
        Task AddCartAsync(Cart cart);
        Task AddOrUpdateCartItemAsync(CartItem cartItem);
        Task UpdateItemQuantityAsync(Guid cartItemId, int quantity);
        Task RemoveItemFromCartAsync(Guid cartItemId);
        Task ClearCartAsync(Guid cartId);
    }
}

