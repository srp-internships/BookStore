using CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Domain.UnitTests.xUnit.Entities
{
    public class BookSellerTests
    {
        [Fact]
        public void BookSeller_ShouldInitializeWithDefaultValues()
        {
            // Arrange
            var bookSeller = new BookSeller();

            // Act & Assert
            Assert.NotNull(bookSeller);
            Assert.Equal(Guid.Empty, bookSeller.BookId);
            Assert.Equal(Guid.Empty, bookSeller.SellerId);
            Assert.Equal(0m, bookSeller.Price);
        }

        [Fact]
        public void BookSeller_ShouldSetAndGetPropertiesCorrectly()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var sellerId = Guid.NewGuid();
            var price = 19.99m;

            var bookSeller = new BookSeller
            {
                BookId = bookId,
                SellerId = sellerId,
                Price = price
            };

            // Act
            var bookIdResult = bookSeller.BookId;
            var sellerIdResult = bookSeller.SellerId;
            var priceResult = bookSeller.Price;

            // Assert
            Assert.Equal(bookId, bookIdResult);
            Assert.Equal(sellerId, sellerIdResult);
            Assert.Equal(price, priceResult);
        }
    }
}