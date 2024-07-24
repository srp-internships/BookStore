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
            return await _context.Set<Cart>()
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task AddCartAsync(Cart cart)
        {
            await _context.Set<Cart>().AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task AddOrUpdateCartItemAsync(CartItem cartItem)
        {
            var existingItem = await _context.Set<CartItem>()
                .FirstOrDefaultAsync(ci => ci.CartId == cartItem.CartId && ci.BookId == cartItem.BookId);

            if (existingItem != null)
            {
                existingItem.Quantity += cartItem.Quantity;
                _context.Set<CartItem>().Update(existingItem);
            }
            else
            {
                await _context.Set<CartItem>().AddAsync(cartItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemQuantityAsync(Guid cartItemId, int quantity)
        {
            var cartItem = await _context.Set<CartItem>().FindAsync(cartItemId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _context.Set<CartItem>().Update(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveItemFromCartAsync(Guid cartItemId)
        {
            var cartItem = await _context.Set<CartItem>().FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.Set<CartItem>().Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(Guid cartId)
        {
            var cartItems = await _context.Set<CartItem>()
                .Where(ci => ci.CartId == cartId)
                .ToListAsync();

            if (cartItems.Any())
            {
                _context.Set<CartItem>().RemoveRange(cartItems);
                await _context.SaveChangesAsync();
            }
        }
    }
}
