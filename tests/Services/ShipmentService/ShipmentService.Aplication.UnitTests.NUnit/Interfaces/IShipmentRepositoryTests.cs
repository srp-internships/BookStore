using Microsoft.EntityFrameworkCore;
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
    public class ShipmentRepositoryTests : IDisposable
    {
        private ShipmentContext _context;
        private IShipmentRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ShipmentContext>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

            _context = new ShipmentContext(options);
            _repository = new ShipmentRepository(_context);

            // Убедитесь, что база данных пуста
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            // Освобождение ресурсов
            _context?.Dispose();
        }

        [Test]
        public async Task Should_Add_Shipment()
        {
            // Arrange
            var shipment = new Shipment
            {
                ShipmentId = Guid.NewGuid(),
                OrderId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                ShippingAddress = new ShippingAddress
                {
                    Id = Guid.NewGuid(),
                    Street = "123 Main St",
                    City = "Springfield",
                    Country = "USA"
                },
                Items = new List<ShipmentItem>
                {
                    new ShipmentItem { ItemId = Guid.NewGuid(), BookName = "Book 1", Quantity = 1 }
                },
                Status = Status.Shipped
            };

            // Act
            await _repository.AddShipmentAsync(shipment);
            await _repository.SaveChangesAsync();

            // Assert
            var addedShipment = await _context.Shipments.FindAsync(shipment.ShipmentId);
            Assert.NotNull(addedShipment);
            Assert.AreEqual(shipment.OrderId, addedShipment.OrderId);
        }

        [Test]
        public async Task Should_Get_All_Shipments()
        {
            // Arrange
            var shipment1 = new Shipment
            {
                ShipmentId = Guid.NewGuid(),
                OrderId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                ShippingAddress = new ShippingAddress
                {
                    Id = Guid.NewGuid(),
                    Street = "123 Main St",
                    City = "Springfield",
                    Country = "USA"
                },
                Items = new List<ShipmentItem>
        {
            new ShipmentItem { ItemId = Guid.NewGuid(), BookName = "Book 1", Quantity = 1 }
        },
                Status = Status.Shipped
            };

            var shipment2 = new Shipment
            {
                ShipmentId = Guid.NewGuid(),
                OrderId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                ShippingAddress = new ShippingAddress
                {
                    Id = Guid.NewGuid(),
                    Street = "456 Elm St",
                    City = "Shelbyville",
                    Country = "USA"
                },
                Items = new List<ShipmentItem>
        {
            new ShipmentItem { ItemId = Guid.NewGuid(), BookName = "Book 2", Quantity = 2 }
        },
                Status = Status.Pending
            };

            await _repository.AddShipmentAsync(shipment1);
            await _repository.AddShipmentAsync(shipment2);
            await _repository.SaveChangesAsync();

            // Act
            var shipments = await _repository.GetAllShipmentsAsync();

            // Assert
            var shipmentList = shipments.ToList();
            Assert.AreEqual(2, shipmentList.Count, "The number of shipments should be 2.");
            Assert.Contains(shipment1, shipmentList);
            Assert.Contains(shipment2, shipmentList);
        }

        [Test]
        public async Task Should_Get_Shipment_By_Id()
        {
            // Arrange
            var shipment = new Shipment
            {
                ShipmentId = Guid.NewGuid(),
                OrderId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                ShippingAddress = new ShippingAddress
                {
                    Id = Guid.NewGuid(),
                    Street = "123 Main St",
                    City = "Springfield",
                    Country = "USA"
                },
                Items = new List<ShipmentItem>
                {
                    new ShipmentItem { ItemId = Guid.NewGuid(), BookName = "Book 1", Quantity = 1 }
                },
                Status = Status.Shipped
            };

            await _repository.AddShipmentAsync(shipment);
            await _repository.SaveChangesAsync();

            // Act
            var retrievedShipment = await _repository.GetShipmentByIdAsync(shipment.ShipmentId);

            // Assert
            Assert.NotNull(retrievedShipment);
            Assert.AreEqual(shipment.ShipmentId, retrievedShipment.ShipmentId);
        }

        [Test]
        public async Task Should_Update_Shipment()
        {
            // Arrange
            var shipment = new Shipment
            {
                ShipmentId = Guid.NewGuid(),
                OrderId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                ShippingAddress = new ShippingAddress
                {
                    Id = Guid.NewGuid(),
                    Street = "123 Main St",
                    City = "Springfield",
                    Country = "USA"
                },
                Items = new List<ShipmentItem>
                {
                    new ShipmentItem { ItemId = Guid.NewGuid(), BookName = "Book 1", Quantity = 1 }
                },
                Status = Status.Shipped
            };

            await _repository.AddShipmentAsync(shipment);
            await _repository.SaveChangesAsync();

            // Act
            shipment.Status = Status.Delivered;
            await _repository.UpdateShipmentAsync(shipment);
            await _repository.SaveChangesAsync();

            // Assert
            var updatedShipment = await _repository.GetShipmentByIdAsync(shipment.ShipmentId);
            Assert.NotNull(updatedShipment);
            Assert.AreEqual(Status.Delivered, updatedShipment.Status);
        }

        // Реализация IDisposable для освобождения ресурсов
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
