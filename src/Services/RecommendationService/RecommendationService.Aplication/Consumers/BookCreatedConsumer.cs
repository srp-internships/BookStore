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

            foreach (var authorId in bookCreatedEvent.AuthorIds)
            {
                var newBook = new Book
                {
                    Id = Guid.NewGuid(),
                    Title = bookCreatedEvent.Title,
                    AuthorId = authorId,
                };

                foreach (var categoryId in bookCreatedEvent.CategoryIds)
                {
                    var bookCategory = new BookCategory
                    {
                        BookId = newBook.Id,
                        CategoryId = categoryId
                    };
                    newBook.CategoriesIds.Add(bookCategory.CategoryId);
                }

                await _dbContext.Books.AddAsync(newBook);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
