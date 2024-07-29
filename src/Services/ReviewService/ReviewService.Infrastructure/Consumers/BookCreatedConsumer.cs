using CatalogService.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using ReviewService.Domain.Entities;
using ReviewService.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Infrastructure.Consumers
{
    public class BookCreatedConsumer : IConsumer<BookCreatedEvent>
    {
        private readonly ILogger<BookCreatedConsumer> _logger;
        private readonly ReviewDbContext _context;

        public BookCreatedConsumer(ILogger<BookCreatedConsumer> logger, ReviewDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task Consume(ConsumeContext<BookCreatedEvent> context)
        {
            _logger.LogInformation("Book Created: {Id}",
                context.Message.Id);

            var book = new Book
            {
                Id = context.Message.Id,

            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
    }
}
