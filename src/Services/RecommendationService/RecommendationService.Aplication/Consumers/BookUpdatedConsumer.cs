using CatalogService.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using RecommendationService.Application.Interfaces;
using RecommendationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationService.Application.Consumers
{
    public class BookUpdatedConsumer : IConsumer<BookUpdatedEvent>
    {
        private readonly IApplicationDbContext _dbContext;

        public BookUpdatedConsumer(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<BookUpdatedEvent> context)
        {
           var bookUpdatedEvent = context.Message;

            var bookUpdated = await _dbContext.Books.FirstOrDefaultAsync(_=>_.Id==bookUpdatedEvent.Id);
            if (bookUpdated != null)
            {
                bookUpdated.AuthorId = bookUpdatedEvent.AuthorIds.FirstOrDefault();
                //bookUpdated.CategoriesIds = bookUpdatedEvent.CategoryIds;
                bookUpdated.Title = bookUpdatedEvent.Title;
            }
            else throw new Exception("not this book");

            _dbContext.Books.Update(bookUpdated);
           
            await _dbContext.SaveChangesAsync();
        }
    }
}
