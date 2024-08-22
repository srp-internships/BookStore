using AnalyticService.Domain.Entities;
using AnalyticsService.Infrastructure.Data;
using MassTransit;
using OrderService.IntegrationEvents;


namespace AnalyticsService.Infrastructure.Consumers
{
    public class BooksPurchasedConsumer : IConsumer<OrderProcessedIntegrationEvent>
    {
        private readonly AnalyticsDbContext _dbContext;

        public BooksPurchasedConsumer(AnalyticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<OrderProcessedIntegrationEvent> context)
        {
            var orderProcessedEvent = context.Message;

            foreach (var book in orderProcessedEvent.Items)
            {
                var bookSale = new BookSale
                {
                    Id = Guid.NewGuid(),
                    BookId = book.BookId,
                    Quantity = book.Quantity,
                    PurchaseDate = DateTime.UtcNow,
                    Title = book.Title,
                    SellerId = book.SellerId,
                };

                await _dbContext.BookSales.AddAsync(bookSale);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
