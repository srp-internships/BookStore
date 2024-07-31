using CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Domain.UnitTests.xUnit.Entities
{
    public class CartItemTests
    {
        [Fact]
        public void CartItem_ShouldInitializeWithDefaultValues()
        {
            // Arrange
            var cartItem = new CartItem();

            // Act & Assert
            Assert.NotNull(cartItem);
            Assert.Equal(Guid.Empty, cartItem.Id);
            Assert.Equal(Guid.Empty, cartItem.BookId);
            Assert.Equal(Guid.Empty, cartItem.CartId);
            Assert.Null(cartItem.BookName);
            Assert.Null(cartItem.ImageUrl);
            Assert.Equal(0m, cartItem.Price);
            Assert.Equal(0, cartItem.Quantity);
            Assert.Equal(Guid.Empty, cartItem.SellerId);
        }

        [Fact]
        public void CartItem_ShouldSetAndGetPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var cartId = Guid.NewGuid();
            var bookName = "Sample Book";
            var imageUrl = "sample-image-url";
            var price = 19.99m;
            var quantity = 3;
            var sellerId = Guid.NewGuid();

            var cartItem = new CartItem
            {
                Id = id,
                BookId = bookId,
                CartId = cartId,
                BookName = bookName,
                ImageUrl = imageUrl,
                Price = price,
                Quantity = quantity,
                SellerId = sellerId
            };

            // Act
            var idResult = cartItem.Id;
            var bookIdResult = cartItem.BookId;
            var cartIdResult = cartItem.CartId;
            var bookNameResult = cartItem.BookName;
            var imageUrlResult = cartItem.ImageUrl;
            var priceResult = cartItem.Price;
            var quantityResult = cartItem.Quantity;
            var sellerIdResult = cartItem.SellerId;

            // Assert
            Assert.Equal(id, idResult);
            Assert.Equal(bookId, bookIdResult);
            Assert.Equal(cartId, cartIdResult);
            Assert.Equal(bookName, bookNameResult);
            Assert.Equal(imageUrl, imageUrlResult);
            Assert.Equal(price, priceResult);
            Assert.Equal(quantity, quantityResult);
            Assert.Equal(sellerId, sellerIdResult);
        }
    }
}
