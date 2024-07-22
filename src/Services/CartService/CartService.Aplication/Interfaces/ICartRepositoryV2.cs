namespace CartService.Aplication.Interfaces
{
    public interface ICartRepositoryV2
    {
        Task<Cart> GetCartByUserIdAsync(Guid userId);
        Task AddItemToCartAsync(Guid userId, CartItem item);
        Task UpdateCartItemAsync(Guid userId, CartItem item);
        Task RemoveItemFromCartAsync(Guid userId, Guid itemId);
        Task<decimal> GetCartTotalAsync(Guid userId);
    }
}
