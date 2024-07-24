using CartService.Domain.Entities;
using CartService.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Infrastructure.UnitTests.xUnit.Persistence.Configurations
{
    public class BookSellerConfigurationTests
    {
        //CreateNewContextOptions:
        //Method for creating new context options using an in-memory database.
        private DbContextOptions<CartDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<CartDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            
        }

        [Fact]
        public void BookSeller_HasCompositeKey_BookId_SellerId()
        {
            // Arrange
            var options = CreateNewContextOptions();

            // Act
            using (var context = new CartDbContext(options))
            {
                var entityType = context.Model.FindEntityType(typeof(BookSeller));
                var primaryKey = entityType.FindPrimaryKey();

                // Assert
                Assert.NotNull(primaryKey);
                Assert.Equal(2, primaryKey.Properties.Count);
                Assert.Contains(primaryKey.Properties, p => p.Name == "BookId");
                Assert.Contains(primaryKey.Properties, p => p.Name == "SellerId");
            }
        }
    }
}
