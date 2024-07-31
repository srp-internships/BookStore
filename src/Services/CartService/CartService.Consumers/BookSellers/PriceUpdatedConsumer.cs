using Microsoft.EntityFrameworkCore;

namespace CartService.Consumers.BookSellers
{
    public class PriceUpdatedConsumer : IConsumer<PriceUpdatedEvent>
    {
        private readonly ILogger<PriceUpdatedConsumer> _logger;
        private readonly CartDbContext _context;

        public PriceUpdatedConsumer(ILogger<PriceUpdatedConsumer> logger, CartDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Consume(ConsumeContext<PriceUpdatedEvent> context)
        {
            _logger.LogInformation("Price Updated: {BookId}, {SellerId}, {Price}",
                context.Message.BookId, context.Message.SellerId, context.Message.Price);

            // Обновление цены книги
            var price = await _context.BookSellers.FirstOrDefaultAsync(
                p => p.BookId == context.Message.BookId &&
                p.SellerId == context.Message.SellerId);

            if (price != null)
            {
                price.Price = context.Message.Price;
                _context.BookSellers.Update(price);
            }

            // Обновление элементов корзины с новой ценой
            var cartItems = await _context.Items
                .Where(ci => ci.BookId == context.Message.BookId && ci.SellerId == context.Message.SellerId)
                .ToListAsync();

            foreach (var cartItem in cartItems)
            {
                cartItem.Price = context.Message.Price;
            }
            await _context.SaveChangesAsync();
        }
    }
}
