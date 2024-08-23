using CartService.Aplication.Commons.DTOs;
using CartService.Aplication.Commons.Interfaces;
using CartService.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Aplication.UnitTests.NUnit.Interfaces
{
    [TestFixture]
    public class ICartServiceTests
    {
        private Mock<ICartService> _cartServiceMock;

        [SetUp]
        public void Setup()
        {
            _cartServiceMock = new Mock<ICartService>();
        }
        [Test]
        public async Task GetCartByUserIdAsync_ShouldReturnCart()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedCart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Items = new List<CartItem>()
            };

            _cartServiceMock.Setup(service => service.GetCartByUserIdAsync(userId))
                            .ReturnsAsync(expectedCart);

            // Act
            var result = await _cartServiceMock.Object.GetCartByUserIdAsync(userId);

            // Assert
            Assert.Equals(expectedCart, result);
        }
        [Test]
        public async Task AddToCartAsync_ShouldNotThrowException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = new AddToCartRequest
            {
                BookId = Guid.NewGuid(),
                Quantity = 1
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(() => _cartServiceMock.Object.AddToCartAsync(userId, request));
        }
        [Test]
        public async Task UpdateCartItemQuantityAsync_ShouldNotThrowException()
        {
            // Arrange
            var cartItemId = Guid.NewGuid();
            var quantity = 2;

            // Act & Assert
            Assert.DoesNotThrowAsync(() => _cartServiceMock.Object.UpdateCartItemQuantityAsync(cartItemId, quantity));
        }
        [Test]
        public async Task RemoveCartItemAsync_ShouldNotThrowException()
        {
            // Arrange
            var cartItemId = Guid.NewGuid();

            // Act & Assert
            Assert.DoesNotThrowAsync(() => _cartServiceMock.Object.RemoveCartItemAsync(cartItemId));
        }
        [Test]
        public async Task ClearCartAsync_ShouldNotThrowException()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act & Assert
            Assert.DoesNotThrowAsync(() => _cartServiceMock.Object.ClearCartAsync(userId));
        }
        [Test]
        public async Task GetTotalPriceAsync_ShouldReturnTotalPrice()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedTotalPrice = 100.0m;

            _cartServiceMock.Setup(service => service.GetTotalPriceAsync(userId))
                            .ReturnsAsync(expectedTotalPrice);

            // Act
            var result = await _cartServiceMock.Object.GetTotalPriceAsync(userId);

            // Assert
            Assert.Equals(expectedTotalPrice, result);
        }
        [Test]
        public async Task IsBookAvailableAsync_ShouldReturnAvailabilityStatus()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var expectedAvailability = true;

            _cartServiceMock.Setup(service => service.IsBookAvailableAsync(bookId))
                            .ReturnsAsync(expectedAvailability);

            // Act
            var result = await _cartServiceMock.Object.IsBookAvailableAsync(bookId);

            // Assert
            Assert.Equals(expectedAvailability, result);
        }

    }
}
