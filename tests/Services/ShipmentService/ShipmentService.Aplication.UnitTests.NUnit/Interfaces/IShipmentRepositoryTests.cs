using Microsoft.EntityFrameworkCore;
using Moq;
using ShipmentService.Aplication.Interfaces;
using ShipmentService.Domain.Entities.Shipments;
using ShipmentService.Domain.Enums;
using ShipmentService.Infrastructure.Persistence.DbContexts;
using ShipmentService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.UnitTests.NUnit.Interfaces
{
    [TestFixture]
    public class ShipmentRepositoryTests 
    {
        private Mock<IShipmentRepository> _shipmentRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _shipmentRepositoryMock = new Mock<IShipmentRepository>();
        }

        [Test]
        public async Task GetAllShipmentsAsync_ShouldReturnAllShipments()
        {
            // Arrange
            var shipments = new List<Shipment>
        {
            new Shipment { ShipmentId = Guid.NewGuid(), OrderId = Guid.NewGuid(), CustomerId = Guid.NewGuid() },
            new Shipment { ShipmentId = Guid.NewGuid(), OrderId = Guid.NewGuid(), CustomerId = Guid.NewGuid() }
        };
            _shipmentRepositoryMock.Setup(repo => repo.GetAllShipmentsAsync()).ReturnsAsync(shipments);

            // Act
            var result = await _shipmentRepositoryMock.Object.GetAllShipmentsAsync();

            // Assert
            Assert.AreEqual(shipments.Count, result.Count());
            Assert.AreEqual(shipments, result);
        }

        [Test]
        public async Task GetShipmentByIdAsync_ShouldReturnShipment_WhenShipmentExists()
        {
            // Arrange
            var shipmentId = Guid.NewGuid();
            var shipment = new Shipment { ShipmentId = shipmentId, OrderId = Guid.NewGuid(), CustomerId = Guid.NewGuid() };
            _shipmentRepositoryMock.Setup(repo => repo.GetShipmentByIdAsync(shipmentId)).ReturnsAsync(shipment);

            // Act
            var result = await _shipmentRepositoryMock.Object.GetShipmentByIdAsync(shipmentId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(shipmentId, result?.ShipmentId);
        }

        [Test]
        public async Task GetShipmentByIdAsync_ShouldReturnNull_WhenShipmentDoesNotExist()
        {
            // Arrange
            var shipmentId = Guid.NewGuid();
            _shipmentRepositoryMock.Setup(repo => repo.GetShipmentByIdAsync(shipmentId)).ReturnsAsync((Shipment?)null);

            // Act
            var result = await _shipmentRepositoryMock.Object.GetShipmentByIdAsync(shipmentId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task UpdateShipmentAsync_ShouldUpdateShipment()
        {
            // Arrange
            var shipment = new Shipment { ShipmentId = Guid.NewGuid(), OrderId = Guid.NewGuid(), CustomerId = Guid.NewGuid() };
            _shipmentRepositoryMock.Setup(repo => repo.UpdateShipmentAsync(shipment)).Returns(Task.CompletedTask);

            // Act
            await _shipmentRepositoryMock.Object.UpdateShipmentAsync(shipment);

            // Assert
            _shipmentRepositoryMock.Verify(repo => repo.UpdateShipmentAsync(shipment), Times.Once);
        }

        [Test]
        public async Task SaveChangesAsync_ShouldSaveChanges()
        {
            // Arrange
            _shipmentRepositoryMock.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await _shipmentRepositoryMock.Object.SaveChangesAsync();

            // Assert
            _shipmentRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
    }
}
