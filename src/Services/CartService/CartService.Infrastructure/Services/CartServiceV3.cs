using CartService.Aplication.Interfaces;
using CartService.Infrastructure.Persistence.Contexts;

namespace CartService.Infrastructure.Services
{
    public class CartServiceV3 : ICartServiceV3
    {
        private readonly CartDbContext _context;

        public CartServiceV3(CartDbContext context)
        {
            _context = context;
        }

        public bool UserExists(Guid userId)
        {
            // Реализуйте логику проверки существования пользователя в базе данных
            return true; // Упрощенный пример
        }

        public Cart GetCart(Guid userId)
        {
            var cart = _context.Carts
                .Include(c => c.Items)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }

            return cart;
        }

        public void AddToCart(Guid userId, CartItem item)
        {
            var cart = GetCart(userId);

            var existingItem = cart.Items.FirstOrDefault(i => i.BookId == item.BookId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                item.CartId = cart.Id;
                cart.Items.Add(item);
            }

            _context.SaveChanges();
        }

        public void RemoveFromCart(Guid userId, Guid bookId)
        {
            var cart = GetCart(userId);
            var item = cart.Items.FirstOrDefault(i => i.BookId == bookId);

            if (item != null)
            {
                cart.Items.Remove(item);
                _context.Items.Remove(item);
                _context.SaveChanges();
            }
        }

        public void ClearCart(Guid userId)
        {
            var cart = GetCart(userId);
            _context.Items.RemoveRange(cart.Items);
            cart.Items.Clear();
            _context.SaveChanges();
        }

        public void UpdateQuantity(Guid userId, Guid bookId, int newQuantity)
        {
            var cart = GetCart(userId);
            var item = _context.Items.FirstOrDefault(i => i.BookId == bookId);

            if (item != null)
            {
                if (newQuantity > 0)
                {
                    item.Quantity = newQuantity;
                }
                else
                {
                    cart.Items.Remove(item);
                    _context.Items.Remove(item);
                }
                _context.SaveChanges();
            }
        }
    }

}
