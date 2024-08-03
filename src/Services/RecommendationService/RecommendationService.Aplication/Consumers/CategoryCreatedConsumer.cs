using CatalogService.Contracts;
using MassTransit;
using RecommendationService.Application.Interfaces;
using RecommendationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationService.Application.Consumers
{
    public class CategoryCreatedConsumer : IConsumer<CategoryCreatedEvent>
    {

        private readonly IApplicationDbContext _dbContext;

        public CategoryCreatedConsumer(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<CategoryCreatedEvent> context)
        {
            var categoryCreate = context.Message;
            var newCategory = new Category 
            { 
            Id = categoryCreate.Id,
            Title = categoryCreate.Title,
            };
            _dbContext.Categories.Add(newCategory);
          
            await _dbContext.SaveChangesAsync();
        }
    }
}
