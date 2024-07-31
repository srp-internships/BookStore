using CartService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CartService.Consumers.BookSellers
{
    public class PriceCreatedConsumer : IConsumer<PriceCreatedEvent>
    {
        private readonly ILogger<PriceCreatedConsumer> _logger;
        private readonly CartDbContext _context;

        public PriceCreatedConsumer(ILogger<PriceCreatedConsumer> logger, CartDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task Consume(ConsumeContext<PriceCreatedEvent> context)
        {
            _logger.LogInformation("Price Created: BookId: {BookId}, SellerId: {SellerId}, Price: {Price}",
                context.Message.BookId, context.Message.SellerId, context.Message.Price);

            var price = new BookSeller
            {
                BookId = context.Message.BookId,
                SellerId = context.Message.SellerId,
                Price = context.Message.Price
            };

            _context.BookSellers.Add(price);

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
