using MassTransit;
using RecommendationService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecommendationService.Contracts;
using RecommendationService.Domain.Entities;

namespace RecommendationService.Application.Consumers
{
    public class BookCreatedConsumer: IConsumer<BookCreatedEvent>
    {
        private readonly IApplicationDbContext _dbContext;

        public BookCreatedConsumer(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<BookCreatedEvent> context)
        {
            var bookCreatedEvent = context.Message;

            foreach (var book in bookCreatedEvent.AuthorIds)
            {
                var newBook = new Book
                {
                    Id = Guid.NewGuid(),
                    //Categories = book.Categories
                };

                await _dbContext.Books.AddAsync(newBook);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
