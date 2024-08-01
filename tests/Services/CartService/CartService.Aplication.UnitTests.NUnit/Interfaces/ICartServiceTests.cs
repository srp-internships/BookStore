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
    public class ICartServiceTests
    {
        private Mock<ICartService> _cartServiceMock;

        [SetUp]
        public void Setup()
        {
            _cartServiceMock = new Mock<ICartService>();
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

            _cartServiceMock.Setup(service => service.GetCartAsync(userId))
                            .ReturnsAsync(expectedCart);

            // Act
            var result = await _cartServiceMock.Object.GetCartAsync(userId);

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

            _cartServiceMock.Setup(service => service.AddCartAsync(cart))
                            .Returns(Task.CompletedTask);

            // Act
            await _cartServiceMock.Object.AddCartAsync(cart);

            // Assert
            _cartServiceMock.Verify(service => service.AddCartAsync(cart), Times.Once);
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

            _cartServiceMock.Setup(service => service.AddOrUpdateCartItemAsync(cartItem))
                            .Returns(Task.CompletedTask);

            // Act
            await _cartServiceMock.Object.AddOrUpdateCartItemAsync(cartItem);

            // Assert
            _cartServiceMock.Verify(service => service.AddOrUpdateCartItemAsync(cartItem), Times.Once);
        }
        #endregion

        #region UpdateItemQuantityAsync_ShouldUpdateQuantity
        [Test]
        public async Task UpdateItemQuantityAsync_ShouldUpdateQuantity()
        {
            // Arrange
            var cartItemId = Guid.NewGuid();
            var quantity = 5;

            _cartServiceMock.Setup(service => service.UpdateItemQuantityAsync(cartItemId, quantity))
                            .Returns(Task.CompletedTask);

            // Act
            await _cartServiceMock.Object.UpdateItemQuantityAsync(cartItemId, quantity);

            // Assert
            _cartServiceMock.Verify(service => service.UpdateItemQuantityAsync(cartItemId, quantity), Times.Once);
        }
        #endregion

        #region RemovaItemFromCartAsync_ShouldRemoveItem
        [Test]
        public async Task RemoveItemFromCartAsync_ShouldRemoveItem()
        {
            // Arrange
            var cartItemId = Guid.NewGuid();

            _cartServiceMock.Setup(service => service.RemoveItemFromCartAsync(cartItemId))
                            .Returns(Task.CompletedTask);

            // Act
            await _cartServiceMock.Object.RemoveItemFromCartAsync(cartItemId);

            // Assert
            _cartServiceMock.Verify(service => service.RemoveItemFromCartAsync(cartItemId), Times.Once);
        }
        #endregion

        #region ClearCartAsync_ShouldClearCart
        [Test]
        public async Task ClearCartAsync_ShouldClearCart()
        {
            // Arrange
            var cartId = Guid.NewGuid();

            _cartServiceMock.Setup(service => service.ClearCartAsync(cartId))
                            .Returns(Task.CompletedTask);

            // Act
            await _cartServiceMock.Object.ClearCartAsync(cartId);

            // Assert
            _cartServiceMock.Verify(service => service.ClearCartAsync(cartId), Times.Once);
        }
        #endregion
    }
}
