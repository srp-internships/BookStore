using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyticService.Contracts;
using AnalyticService.Domain.Entities;
using AnalyticsService.Application.Interfaces;
using AnalyticsService.Infrastructure.Data;
using MassTransit;


namespace AnalyticsService.Infrastructure.Consumers
{
    public class BooksPurchasedConsumer : IConsumer<BooksPurchasedEvent>
    {
        private readonly AnalyticsDbContext _dbContext;
        public BooksPurchasedConsumer(AnalyticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<BooksPurchasedEvent> context)
        {
            var booksPurchasedEvent = context.Message;

            foreach (var book in booksPurchasedEvent.Books)
            {
                var bookSale = new BookSale
                {
                    Id = Guid.NewGuid(),
                    BookId = book.BookId,
                    Quantity = book.Quantity,
                    PurchaseDate = DateTime.UtcNow
                };

                await _dbContext.BookSales.AddAsync(bookSale);
            }

            await _dbContext.SaveChangesAsync();
        }

    }
}
