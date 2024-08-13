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
    public class BookConfigurationTests
    {
        private DbContextOptions<CartDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<CartDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public void Book_HasKey_Id()
        {
            // Arrange
            var options = CreateNewContextOptions();

            // Act
            using (var context = new CartDbContext(options))
            {
                var entityType = context.Model.FindEntityType(typeof(Book));
                var primaryKey = entityType.FindPrimaryKey();

                // Assert
                Assert.NotNull(primaryKey);
                Assert.Single(primaryKey.Properties);
                Assert.Equal("Id", primaryKey.Properties[0].Name);
            }
        }
    }
}
