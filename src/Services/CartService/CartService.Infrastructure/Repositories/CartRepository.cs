using CartService.Aplication.Commons.Interfaces;
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
        public async Task<Cart?> GetCartByUserIdAsync(Guid userId)
        {
            return await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
        public async Task<Cart?> GetCartByIdAsync(Guid cartId)
        {
            return await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId);
        }
        public async Task<CartItem?> GetCartItemByIdAsync(Guid cartItemId)
        {
            return await _context.Items
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId);
        }
        public async Task<CartItem?> GetCartItemByBookIdAsync(Guid cartId, Guid bookId)
        {
            return await _context.Items
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.BookId == bookId);
        }
        public async Task AddCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
        }
        public async Task AddCartItemAsync(CartItem cartItem)
        {
            await _context.Items.AddAsync(cartItem);
        }
        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            _context.Items.Update(cartItem);
            await Task.CompletedTask; 
        }
        public async Task DeleteCartAsync(Guid cartId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart != null)
            {
                _context.Items.RemoveRange(cart.Items);
                _context.Carts.Remove(cart);
            }
        }
        public async Task DeleteCartItemAsync(Guid cartItemId)
        {
            var cartItem = await _context.Items
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId);

            if (cartItem != null)
            {
                _context.Items.Remove(cartItem);
            }
        }
    }
}