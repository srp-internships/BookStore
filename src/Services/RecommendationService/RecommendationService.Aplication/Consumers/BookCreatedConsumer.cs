using MassTransit;
using RecommendationService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecommendationService.Domain.Entities;
using CatalogService.Contracts;

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

            var newBook = new Book 
            { 
            Id= bookCreatedEvent.Id,
            AuthorId = bookCreatedEvent.AuthorIds.First(),
            CategoriesIds=bookCreatedEvent.CategoryIds,
            Title=bookCreatedEvent.Title,
            };
            
            _dbContext.Books.Add(newBook);
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
