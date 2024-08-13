using FluentAssertions;
using ShipmentService.Domain.Entities.Shipments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Domain.UnitTests.xUnit.Entities
{
    
    public class ShipmentItemTests
    {
        [Fact]
        public void ShipmentItem_ShouldInitializeWithDefaultValues()
        {
            //Arrage
            var shipmentItem = new ShipmentItem();

            //Act and Assert
           shipmentItem.ItemId.Should().Be(Guid.Empty);
           shipmentItem.BookName.Should().BeNull();
           shipmentItem.Quantity.Should().Be(1);

        }
        [Fact]
        public void ShipmentItem_ShouldSetPropertiesCorrectly()
        {
            //Arrage
            var itemId = Guid.NewGuid();
            var bookName = "Test Name Book";
            var quantity = 10;

            var shipmentItem = new ShipmentItem
            {
                ItemId = itemId,
                BookName = bookName,
                Quantity = quantity,
            };
            //Act and Assert
            shipmentItem.ItemId.Should().Be(itemId);
            shipmentItem.BookName.Should().Be(bookName);
            shipmentItem.Quantity.Should().Be(quantity);

        }
        [Fact]
        public void ShipmentItem_ShouldRequireBookName()
        {
            // Arrange
            var shipmentItem = new ShipmentItem
            {
                ItemId = Guid.NewGuid(),
                Quantity = 5
            };

            // Act
            var validationContext = new ValidationContext(shipmentItem);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(shipmentItem, validationContext, validationResults, true);

            // Assert
            isValid.Should().BeFalse();
            validationResults.Should().ContainSingle(vr => vr.ErrorMessage == "Title is required");
        }
    }
}
