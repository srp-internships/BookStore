using CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Domain.UnitTests.xUnit.Entities
{
    public class CartTests
    {
        [Fact]
        public void Cart_ShouldInitializeWithDefaultValues()
        {
            // Arrange
            var cart = new Cart();

            // Act & Assert
            Assert.NotNull(cart);
            Assert.Equal(Guid.Empty, cart.Id);
            Assert.Equal(Guid.Empty, cart.UserId);
            Assert.NotNull(cart.Items);
            Assert.Empty(cart.Items);
            Assert.Equal(0m, cart.TotalPrice);
        }

        [Fact]
        public void Cart_ShouldCalculateTotalPriceCorrectly()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var sellerId = Guid.NewGuid();

            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Items = new List<CartItem>
            {
                new CartItem 
                { 
                    BookId = bookId, 
                    CartId = Guid.NewGuid(), 
                    BookName = "Book 1", 
                    ImageUrl = "image1.jpg", 
                    Price = 10m, 
                    Quantity = 2, 
                    SellerId = sellerId 
                },
                new CartItem 
                { 
                    BookId = bookId, 
                    CartId = Guid.NewGuid(), 
                    BookName = "Book 2", 
                    ImageUrl = "image2.jpg", 
                    Price = 15m, 
                    Quantity = 1, 
                    SellerId = sellerId 
                }
            }
        };

            // Act
            var totalPrice = cart.TotalPrice;

            // Assert
            var expectedTotalPrice = (10m * 2) + (15m * 1);
            Assert.Equal(expectedTotalPrice, totalPrice);
        }
    }
}
