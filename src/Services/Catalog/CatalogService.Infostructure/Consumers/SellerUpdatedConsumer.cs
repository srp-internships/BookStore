using CatalogService.Domain.Entities;
using IdentityService.IntegrationEvents.SellerEvents;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infostructure.Consumers
{
    public class SellerUpdatedConsumer(
        CatalogDbContext dbContext) : IConsumer<SellerUpdatedIntegrationEvent>
    {
        private readonly CatalogDbContext _dbContext = dbContext;

        public async Task Consume(ConsumeContext<SellerUpdatedIntegrationEvent> context)
        {
            var seller = new Seller
            {
                Id = context.Message.SellerId,
                Name = context.Message.FirstName
            };
            await _dbContext.Sellers.AddAsync(seller);
            await _dbContext.SaveChangesAsync();
        }
    }
}
