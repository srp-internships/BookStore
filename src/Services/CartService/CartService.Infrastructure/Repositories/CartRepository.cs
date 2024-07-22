using CartService.Aplication.Interfaces;
using CartService.Infrastructure.Persistence.Contexts;

namespace CartService.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly CartDbContext _context;

        public CartRepository(CartDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartAsync(Guid userId)
        {
            return await _context.Carts
                .Include(c => c.Items)
                .SingleOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task AddCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
        }

        public async Task AddOrUpdateCartItemAsync(CartItem cartItem)
        {
            var existingCartItem = await _context.Items
                .SingleOrDefaultAsync(ci => ci.Id == cartItem.Id);

            if (existingCartItem == null)
            {
                _context.Items.Add(cartItem);
            }
            else
            {
                // Обновление существующего элемента
                existingCartItem.Price = cartItem.Price;
                existingCartItem.BookName = cartItem.BookName;
                existingCartItem.ImageUrl = cartItem.ImageUrl;
                existingCartItem.Quantity = cartItem.Quantity; // Убедитесь, что Quantity обновляется
                _context.Entry(existingCartItem).State = EntityState.Modified;
            }

            // Не вызываем здесь SaveChangesAsync
        }

        public async Task<CartItem> GetCartItemAsync(Guid cartItemId)
        {
            return await _context.Items.FindAsync(cartItemId);
        }

        public async Task RemoveCartItemAsync(Guid cartItemId)
        {
            var cartItem = await _context.Items.FindAsync(cartItemId);

            if (cartItem != null)
            {
                _context.Items.Remove(cartItem);
            }

            // Не вызываем здесь SaveChangesAsync
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateQuantity(Guid userId, Guid bookId, int newQuantity)
        {
            // Изменено, чтобы использовать метод SaveChangesAsync в конце
            var cart = _context.Carts
                .Include(c => c.Items)
                .SingleOrDefault(c => c.UserId == userId);

            if (cart != null)
            {
                var cartItem = cart.Items.SingleOrDefault(i => i.BookId == bookId);
                if (cartItem != null)
                {
                    cartItem.Quantity = newQuantity;
                    _context.Entry(cartItem).State = EntityState.Modified;
                }

                // Вызываем SaveChangesAsync в конце
                _context.SaveChangesAsync().Wait(); // Используйте await при вызове метода в асинхронной среде
            }
        }
    }
}
