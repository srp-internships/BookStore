using CartService.Infrastructure.Persistence.Contexts;
using CatalogService.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

            var price = await _context.BookSellers.FirstOrDefaultAsync(
                p => p.BookId == context.Message.BookId && 
                p.SellerId == context.Message.SellerId);

            if (price != null)
            {
                price.Price = context.Message.Price;

                _context.BookSellers.Update(price);
                await _context.SaveChangesAsync();
            }
        }
    }
}
