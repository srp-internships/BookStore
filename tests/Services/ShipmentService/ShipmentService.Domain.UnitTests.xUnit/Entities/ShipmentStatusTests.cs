using ShipmentService.Domain.Enums;
using FluentAssertions;

namespace ShipmentService.Domain.UnitTests.xUnit.Entities
{
    public class ShipmentStatusTests
    {
        [Fact]
        public void Status_Values_ShouldHaveExpectedValues()
        {
            // Assert
            ((int)Status.Pending).Should().Be(0);
            ((int)Status.Shipped).Should().Be(1);
            ((int)Status.Delivered).Should().Be(2);
        }

        [Fact]
        public void Status_ShouldHaveCorrectNames()
        {
            // Assert
            Status.Pending.ToString().Should().Be("Pending");
            Status.Shipped.ToString().Should().Be("Shipped");
            Status.Delivered.ToString().Should().Be("Delivered");
        }

        [Fact]
        public void Status_ShouldHaveDefinedValues()
        {
            // Arrange
            var expectedValues = new[]
            {
            (Status.Pending, 0),
            (Status.Shipped, 1),
            (Status.Delivered, 2)
        };

            // Act & Assert
            foreach (var (status, expectedValue) in expectedValues)
            {
                ((int)status).Should().Be(expectedValue); 
            }
        }
    }
}
