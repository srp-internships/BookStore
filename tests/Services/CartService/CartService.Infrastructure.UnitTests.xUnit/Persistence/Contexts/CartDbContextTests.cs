using CartService.Domain.Entities;
using CartService.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Infrastructure.UnitTests.xUnit.Persistence.Contexts
{
    public class CartDbContextTests
    {
        private DbContextOptions<CartDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<CartDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        #region Can_Add_Cart
        [Fact]
        public void Can_Add_Cart()
        {
            // Arrange
            var options = CreateNewContextOptions();

            using (var context = new CartDbContext(options))
            {
                var cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid()
                };

                // Act
                context.Carts.Add(cart);
                context.SaveChanges();
            }

            // Assert
            using (var context = new CartDbContext(options))
            {
                Assert.Equal(1, context.Carts.Count());
            }
        }
        #endregion

        #region Can_Add_CartItem
        [Fact]
        public void Can_Add_CartItem()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var cartId = Guid.NewGuid();

            using (var context = new CartDbContext(options))
            {
                var cart = new Cart
                {
                    Id = cartId,
                    UserId = Guid.NewGuid()
                };

                context.Carts.Add(cart);
                context.SaveChanges();
            }

            using (var context = new CartDbContext(options))
            {
                var cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    BookId = Guid.NewGuid(),
                    CartId = cartId,
                    BookName = "Sample Book",
                    ImageUrl = "sample-image-url",
                    Price = 19.99m,
                    Quantity = 2,
                    SellerId = Guid.NewGuid()
                };

                // Act
                context.Items.Add(cartItem);
                context.SaveChanges();
            }

            // Assert
            using (var context = new CartDbContext(options))
            {
                Assert.Equal(1, context.Items.Count());
            }
        }
        #endregion

        #region Can_Query_CartItems
        [Fact]
        public void Can_Query_CartItems()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var cartId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            using (var context = new CartDbContext(options))
            {
                var cart = new Cart
                {
                    Id = cartId,
                    UserId = userId
                };
                context.Carts.Add(cart);

                var cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    BookId = Guid.NewGuid(),
                    CartId = cartId,
                    BookName = "Sample Book",
                    ImageUrl = "sample-image-url",
                    Price = 19.99m,
                    Quantity = 2,
                    SellerId = Guid.NewGuid()
                };
                context.Items.Add(cartItem);
                context.SaveChanges();
            }

            // Act
            using (var context = new CartDbContext(options))
            {
                var items = context.Items.ToList();

                // Assert
                Assert.Single(items);
                Assert.Equal("Sample Book", items.First().BookName);
            }
        }
        #endregion
    }
}
