using ShipmentService.Domain.Entities.Shipments;
using System;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Domain.UnitTests.xUnit.Entities
{
    public class ShippingAddressTests
    {
        [Fact]
        public void ShippingAddress_ShouldSetAndGetProperties()
        {
            // Arrange
            var address = new ShippingAddress();
            var testId = Guid.NewGuid();
            var testStreet = "123 Elm Street";
            var testCity = "Gotham";
            var testCountry = "USA";

            // Act
            address.Id = testId;
            address.Street = testStreet;
            address.City = testCity;
            address.Country = testCountry;

            // Assert
            address.Id.Should().Be(testId);
            address.Street.Should().Be(testStreet);
            address.City.Should().Be(testCity);
            address.Country.Should().Be(testCountry);
        }
        [Fact]
        public void ShippingAddress_Street_ShouldNotExceedMaxLength()
        {
            // Arrange
           

            var address = new ShippingAddress();
            var longStreet = new string('A', 101);
            var longCity = new string('A', 40);
            var longCountry = new string('A', 30);
            // Act
            address.Street = longStreet;
            address.City= longCity;
            address.Country = longCountry;
            // Assert
            address.Street.Should().HaveLength(101); 
            address.City.Should().HaveLength(40);
            address.Country.Should().HaveLength(30);
        }
    }
}
