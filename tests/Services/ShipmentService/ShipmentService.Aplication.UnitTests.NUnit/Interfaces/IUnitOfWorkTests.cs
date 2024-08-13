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
    public class UnitOfWorkTests:IDisposable
    {
        private Mock<IShipmentRepository> _mockShipmentRepository;
        private UnitOfWork _unitOfWork;
        private DbContextOptions<ShipmentContext> _dbContextOptions;
        private ShipmentContext _context;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ShipmentContext>()
                .UseInMemoryDatabase(databaseName: "ShipmentDatabase")
                .Options;

            _context = new ShipmentContext(_dbContextOptions);

            _mockShipmentRepository = new Mock<IShipmentRepository>();

            _unitOfWork = new UnitOfWork(_context, _mockShipmentRepository.Object);
        }

        [Test]
        public void Should_Initialize_UnitOfWork_With_Repository()
        {
            // Assert
            Assert.IsNotNull(_unitOfWork.Shipments);
        }

        [Test]
        public async Task Should_Complete_Async_Save_Changes()
        {
            // Arrange
            var shipment = new Shipment
            {
                ShipmentId = Guid.NewGuid(),
                OrderId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Status = Status.Shipped,
                UpdateShipmentStatus = DateTime.UtcNow,
                OrderStatus = OrderStatus.ShipmentProcessing
            };

            _context.Shipments.Add(shipment);
            _unitOfWork.Shipments.UpdateShipmentAsync(shipment);
            // Act
            var result = await _unitOfWork.CompleteAsync();

            // Assert
            Assert.AreEqual(1, result);
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
