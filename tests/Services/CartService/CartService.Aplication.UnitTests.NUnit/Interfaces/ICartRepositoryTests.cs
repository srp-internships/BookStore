using CartService.Aplication.Interfaces;
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
        #region GetCartAsync_ShouldReturnCart
        [Test]
        public async Task GetCartAsync_ShouldReturnCart()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedCart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Items = new List<CartItem>()
            };

            _cartRepositoryMock.Setup(repo => repo.GetCartAsync(userId))
                               .ReturnsAsync(expectedCart);

            // Act
            var result = await _cartRepositoryMock.Object.GetCartAsync(userId);

            // Assert
            Assert.AreEqual(expectedCart, result);
        }
        #endregion

        #region AddCartAsync_ShouldAddCart
        [Test]
        public async Task AddCartAsync_ShouldAddCart()
        {
            // Arrange
            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Items = new List<CartItem>()
            };

            _cartRepositoryMock.Setup(repo => repo.AddCartAsync(cart))
                               .Returns(Task.CompletedTask);

            // Act
            await _cartRepositoryMock.Object.AddCartAsync(cart);

            // Assert
            _cartRepositoryMock.Verify(repo => repo.AddCartAsync(cart), Times.Once);
        }
        #endregion

        #region AddOrUpdateCartItemAsync_ShouldAddOrUpdateCartItem
        [Test]
        public async Task AddOrUpdateCartItemAsync_ShouldAddOrUpdateCartItem()
        {
            // Arrange
            var cartItem = new CartItem
            {
                Id = Guid.NewGuid(),
                BookId = Guid.NewGuid(),
                CartId = Guid.NewGuid(),
                BookName = "Test Book",
                Price = 10.0m,
                Quantity = 1
            };

            _cartRepositoryMock.Setup(repo => repo.AddOrUpdateCartItemAsync(cartItem))
                               .Returns(Task.CompletedTask);

            // Act
            await _cartRepositoryMock.Object.AddOrUpdateCartItemAsync(cartItem);

            // Assert
            _cartRepositoryMock.Verify(repo => repo.AddOrUpdateCartItemAsync(cartItem), Times.Once);
        }
        #endregion

        #region UpdateItemQuantityAsync_ShouldUpdateQuantity
        [Test]
        public async Task UpdateItemQuantityAsync_ShouldUpdateQuantity()
        {
            // Arrange
            var cartItemId = Guid.NewGuid();
            var quantity = 5;

            _cartRepositoryMock.Setup(repo => repo.UpdateItemQuantityAsync(cartItemId, quantity))
                               .Returns(Task.CompletedTask);

            // Act
            await _cartRepositoryMock.Object.UpdateItemQuantityAsync(cartItemId, quantity);

            // Assert
            _cartRepositoryMock.Verify(repo => repo.UpdateItemQuantityAsync(cartItemId, quantity), Times.Once);
        }
        #endregion

        #region RemoveItemFromCartAsync_ShouldRemoveItem
        [Test]
        public async Task RemoveItemFromCartAsync_ShouldRemoveItem()
        {
            // Arrange
            var cartItemId = Guid.NewGuid();

            _cartRepositoryMock.Setup(repo => repo.RemoveItemFromCartAsync(cartItemId))
                               .Returns(Task.CompletedTask);

            // Act
            await _cartRepositoryMock.Object.RemoveItemFromCartAsync(cartItemId);

            // Assert
            _cartRepositoryMock.Verify(repo => repo.RemoveItemFromCartAsync(cartItemId), Times.Once);
        }
        #endregion

        #region ClearCartAsync_ShouldClearCart
        [Test]
        public async Task ClearCartAsync_ShouldClearCart()
        {
            // Arrange
            var cartId = Guid.NewGuid();

            _cartRepositoryMock.Setup(repo => repo.ClearCartAsync(cartId))
                               .Returns(Task.CompletedTask);

            // Act
            await _cartRepositoryMock.Object.ClearCartAsync(cartId);

            // Assert
            _cartRepositoryMock.Verify(repo => repo.ClearCartAsync(cartId), Times.Once);
        }
        #endregion
    }
}
