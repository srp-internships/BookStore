using CatalogService.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using RecommendationService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationService.Application.Consumers
{
    internal class CategoryUpdatedConsumer : IConsumer<CategoryUpdatedEvent>
    {
       
        
        private readonly IApplicationDbContext _dbContext;

        public CategoryUpdatedConsumer(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<CategoryUpdatedEvent> context)
        {
            var categoryUpdated =  context.Message;
           var category = await _dbContext.Categories.FindAsync(categoryUpdated.Id);
            if (category != null)
            {
                category.Title= categoryUpdated.Title;
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
