using CartService.Domain.Entities;
using CartService.Infrastructure.Persistence.Contexts;
using CatalogService.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

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
            _logger.LogInformation("Price Created: {BookId}, {SellerId}, {Price}",
                context.Message.BookId, context.Message.SellerId, context.Message.Price);

            var price = new BookSeller
            {
                BookId = context.Message.BookId,
                SellerId = context.Message.SellerId,
                Price = context.Message.Price
            };

            _context.BookSellers.Add(price);
            await _context.SaveChangesAsync();
        }
    }
}
