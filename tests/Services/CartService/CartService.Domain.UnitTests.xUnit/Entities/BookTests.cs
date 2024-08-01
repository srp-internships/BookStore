using CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Domain.UnitTests.xUnit.Entities
{
    public class BookTests
    {
        [Fact]
        public void Book_ShouldInitializeWithDefaultValues()
        {
            // Arrange
            var book = new Book();

            // Act & Assert
            Assert.NotNull(book);
            Assert.Equal(Guid.Empty, book.Id);
            Assert.Null(book.Title);
            Assert.Null(book.Image);
        }

        [Fact]
        public void Book_ShouldSetAndGetPropertiesCorrectly()
        {
            // Arrange
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Sample Book",
                Image = "sample-image-url"
            };

            // Act
            var id = book.Id;
            var title = book.Title;
            var image = book.Image;

            // Assert
            Assert.NotEqual(Guid.Empty, id);
            Assert.Equal("Sample Book", title);
            Assert.Equal("sample-image-url", image);
        }
    }
}
