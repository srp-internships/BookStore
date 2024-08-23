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
            try
            {
				var bookCreatedEvent = context.Message;

                var categories = _dbContext.Categories.Where(i => context.Message.CategoryIds.Contains(i.Id)).ToList();
				var newBook = new Book
				{
					Id = bookCreatedEvent.Id,
					AuthorId = bookCreatedEvent.AuthorIds.First(),
					
                    BookCategories = categories,
					Title = bookCreatedEvent.Title,
                    Description = "",
				};

				_dbContext.Books.Add(newBook);

				await _dbContext.SaveChangesAsync();
			}
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
