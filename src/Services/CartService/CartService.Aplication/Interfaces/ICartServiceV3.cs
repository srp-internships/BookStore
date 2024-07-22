namespace CartService.Aplication.Interfaces
{
    public interface ICartServiceV3
    {
        bool UserExists(Guid userId);
        Cart GetCart(Guid userId);
        void AddToCart(Guid userId, CartItem item);
        void RemoveFromCart(Guid userId, Guid bookId);
        void ClearCart(Guid userId);
        void UpdateQuantity(Guid userId, Guid bookId, int newQuantity);
    }
}
