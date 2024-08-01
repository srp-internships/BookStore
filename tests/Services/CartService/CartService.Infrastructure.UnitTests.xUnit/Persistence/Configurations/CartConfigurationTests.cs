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
    public class CartConfigurationTests
    {
        private DbContextOptions<CartDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<CartDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public void Cart_HasKey_Id()
        {
            // Arrange
            var options = CreateNewContextOptions();

            // Act
            using (var context = new CartDbContext(options))
            {
                var entityType = context.Model.FindEntityType(typeof(Cart));
                var primaryKey = entityType.FindPrimaryKey();

                // Assert
                Assert.NotNull(primaryKey);
                Assert.Single(primaryKey.Properties);
                Assert.Equal("Id", primaryKey.Properties[0].Name);
            }
        }

        [Fact]
        public void Cart_HasMany_Items_WithOne_CartId_CascadeOnDelete()
        {
            // Arrange
            var options = CreateNewContextOptions();

            // Act
            using (var context = new CartDbContext(options))
            {
                var entityType = context.Model.FindEntityType(typeof(CartItem));
                var foreignKey = entityType.GetForeignKeys().SingleOrDefault(fk => fk.PrincipalEntityType.ClrType == typeof(Cart) && fk.Properties.Any(p => p.Name == "CartId"));

                // Assert
                Assert.NotNull(foreignKey);
                Assert.Equal(DeleteBehavior.Cascade, foreignKey.DeleteBehavior);
            }
        }

    }
}
