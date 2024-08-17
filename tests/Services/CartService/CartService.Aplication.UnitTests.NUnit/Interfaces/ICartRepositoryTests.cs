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
    public class ICartRepositoryTests
    {
        private Mock<ICartRepository> _cartRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _cartRepositoryMock = new Mock<ICartRepository>();
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

            _cartRepositoryMock.Setup(repo => repo.GetCartByUserIdAsync(userId))
                               .ReturnsAsync(expectedCart);

            // Act
            var result = await _cartRepositoryMock.Object.GetCartByUserIdAsync(userId);

            // Assert
            Assert.Equals(expectedCart, result);
        }
        [Test]
        public async Task GetCartItemByIdAsync_ShouldReturnCartItem()
        {
            // Arrange
            var cartItemId = Guid.NewGuid();
            var expectedCartItem = new CartItem
            {
                Id = cartItemId,
                BookId = Guid.NewGuid(),
                CartId = Guid.NewGuid(),
                Price = 10.0m,
                Quantity = 1
            };

            _cartRepositoryMock.Setup(repo => repo.GetCartItemByIdAsync(cartItemId))
                               .ReturnsAsync(expectedCartItem);

            // Act
            var result = await _cartRepositoryMock.Object.GetCartItemByIdAsync(cartItemId);

            // Assert
            Assert.Equals(expectedCartItem, result);
        }
        [Test]
        public async Task GetCartItemByBookIdAsync_ShouldReturnCartItem()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var expectedCartItem = new CartItem
            {
                Id = Guid.NewGuid(),
                BookId = bookId,
                CartId = cartId,
                Price = 10.0m,
                Quantity = 1
            };

            _cartRepositoryMock.Setup(repo => repo.GetCartItemByBookIdAsync(cartId, bookId))
                               .ReturnsAsync(expectedCartItem);

            // Act
            var result = await _cartRepositoryMock.Object.GetCartItemByBookIdAsync(cartId, bookId);

            // Assert
            Assert.Equals(expectedCartItem, result);
        }
        [Test]
        public async Task AddCartAsync_ShouldNotThrowException()
        {
            // Arrange
            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Items = new List<CartItem>()
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(() => _cartRepositoryMock.Object.AddCartAsync(cart));
        }
        [Test]
        public async Task AddCartItemAsync_ShouldNotThrowException()
        {
            // Arrange
            var cartItem = new CartItem
            {
                Id = Guid.NewGuid(),
                BookId = Guid.NewGuid(),
                CartId = Guid.NewGuid(),
                Price = 10.0m,
                Quantity = 1
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(() => _cartRepositoryMock.Object.AddCartItemAsync(cartItem));
        }
        [Test]
        public async Task UpdateCartItemAsync_ShouldNotThrowException()
        {
            // Arrange
            var cartItem = new CartItem
            {
                Id = Guid.NewGuid(),
                BookId = Guid.NewGuid(),
                CartId = Guid.NewGuid(),
                Price = 10.0m,
                Quantity = 1
            };

            // Act & Assert
            Assert.DoesNotThrowAsync(() => _cartRepositoryMock.Object.UpdateCartItemAsync(cartItem));
        }
        [Test]
        public async Task DeleteCartAsync_ShouldNotThrowException()
        {
            // Arrange
            var cartId = Guid.NewGuid();

            // Act & Assert
            Assert.DoesNotThrowAsync(() => _cartRepositoryMock.Object.DeleteCartAsync(cartId));
        }
        [Test]
        public async Task DeleteCartItemAsync_ShouldNotThrowException()
        {
            // Arrange
            var cartItemId = Guid.NewGuid();

            // Act & Assert
            Assert.DoesNotThrowAsync(() => _cartRepositoryMock.Object.DeleteCartItemAsync(cartItemId));
        }




    }


}
