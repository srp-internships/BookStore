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
    public class CartItemConfigurationTests
    {
        private DbContextOptions<CartDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<CartDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public void CartItem_HasKey_Id()
        {
            // Arrange
            var options = CreateNewContextOptions();

            // Act
            using (var context = new CartDbContext(options))
            {
                var entityType = context.Model.FindEntityType(typeof(CartItem));
                var primaryKey = entityType.FindPrimaryKey();

                // Assert
                Assert.NotNull(primaryKey);
                Assert.Single(primaryKey.Properties);
                Assert.Equal("Id", primaryKey.Properties[0].Name);
            }
        }

        [Fact]
        public void CartItem_HasForeignKey_To_Book()
        {
            // Arrange
            var options = CreateNewContextOptions();

            // Act
            using (var context = new CartDbContext(options))
            {
                var entityType = context.Model.FindEntityType(typeof(CartItem));
                var foreignKey = entityType.GetForeignKeys().SingleOrDefault(fk => fk.PrincipalEntityType.ClrType == typeof(Book) && fk.Properties.Any(p => p.Name == "BookId"));

                // Assert
                Assert.NotNull(foreignKey);
                Assert.Equal(nameof(Book.Id), foreignKey.PrincipalKey.Properties[0].Name);
            }
        }

        [Fact]
        public void CartItem_HasForeignKey_To_Cart()
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
                Assert.Equal(nameof(Cart.Id), foreignKey.PrincipalKey.Properties[0].Name);
            }
        }
    }
}
