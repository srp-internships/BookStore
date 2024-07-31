using CatalogService.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReviewService.Domain.Entities;
using ReviewService.Infrastructure.Persistence.Contexts;
using ReviewService.Infrastructure.Services;
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
            _logger.LogInformation("Book Created: {Id}", context.Message.Id);

            // Check if the book already exists
            var bookExists = await _context.Books.AnyAsync(b => b.Id == context.Message.Id);
            if (bookExists)
            {
                _logger.LogWarning("Book with ID {Id} already exists", context.Message.Id);
                return; 
            }

            var book = new Book
            {
                Id = context.Message.Id,
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
    }
}
