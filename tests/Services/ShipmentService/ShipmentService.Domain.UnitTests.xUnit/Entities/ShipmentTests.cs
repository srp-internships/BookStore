using FluentAssertions;
using ShipmentService.Domain.Entities.Shipments;
using ShipmentService.Domain.Enums;

namespace ShipmentService.Domain.UnitTests.xUnit.Entities
{
    public class ShipmentTests
    {
        [Fact]
        public void Shipment_ShouldInitializeWithDefaultValues()
        {
            // Act
            var shipment = new Shipment();

            // Assert
            shipment.ShipmentId.Should().BeEmpty();
            shipment.Items.Should().NotBeNull();
            shipment.Items.Should().BeEmpty();
            shipment.Status.Should().Be(Status.Pending); // Assuming Status.Initial is a default value
            shipment.UpdateShipmentStatus.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
            shipment.OrderStatus.Should().Be(OrderStatus.Completed); // Assuming OrderStatus.Pending is a default value
        }

        [Fact]
        public void Shipment_ShouldSetAndGetPropertiesCorrectly()
        {
            // Arrange
            var shipmentId = Guid.NewGuid();
            var orderId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var shippingAddress = new ShippingAddress { /* Initialize properties */ };
            var items = new List<ShipmentItem> { new ShipmentItem { /* Initialize properties */ } };
            var status = Status.Shipped;
            var updateShipmentStatus = DateTime.UtcNow;
            var orderStatus = OrderStatus.Completed;

            // Act
            var shipment = new Shipment
            {
                ShipmentId = shipmentId,
                OrderId = orderId,
                CustomerId = customerId,
                ShippingAddress = shippingAddress,
                Items = items,
                Status = status,
                UpdateShipmentStatus = updateShipmentStatus,
                OrderStatus = orderStatus
            };

            // Assert
            shipment.ShipmentId.Should().Be(shipmentId);
            shipment.OrderId.Should().Be(orderId);
            shipment.CustomerId.Should().Be(customerId);
            shipment.ShippingAddress.Should().Be(shippingAddress);
            shipment.Items.Should().BeEquivalentTo(items);
            shipment.Status.Should().Be(status);
            shipment.UpdateShipmentStatus.Should().Be(updateShipmentStatus);
            shipment.OrderStatus.Should().Be(orderStatus);
        }
    }
}
