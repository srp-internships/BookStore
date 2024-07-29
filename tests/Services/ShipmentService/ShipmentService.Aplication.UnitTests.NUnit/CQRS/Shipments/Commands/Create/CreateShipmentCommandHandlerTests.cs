using AutoMapper;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Create;
using ShipmentService.Aplication.Interfaces;
using ShipmentService.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using ShipmentService.Domain.Entities.Shipments;

namespace ShipmentService.Aplication.UnitTests.NUnit.CQRS.Shipments.Commands.Create
{
    [TestFixture]
    public class CreateShipmentCommandHandlerTests
    {
        private Mock<IShipmentRepository> _shipmentRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private CreateShipmentCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _shipmentRepositoryMock = new Mock<IShipmentRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new CreateShipmentCommandHandler(_shipmentRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Handle_ShouldCreateShipmentAndReturnShipmentId()
        {
            // Arrange
            var command = new CreateShipmentCommand
            (
                OrderId: Guid.NewGuid(),
                CustomerId: Guid.NewGuid(),
                ShippingAddress: new ShippingAddress
                {
                    Street = "I.Somoni 123",
                    City = "Khuajand",
                    Country = "Tajikistan"
                },
                Items: new List<ShipmentItem>
                {
                    new ShipmentItem
                    {
                        ItemId = Guid.NewGuid(),
                        Quantity = 2,
                        BookName = "Book Name"
                    }
                }
            );

            var shipment = new Shipment
            {
                ShipmentId = Guid.NewGuid(),
                OrderId = command.OrderId,
                CustomerId = command.CustomerId,
                ShippingAddress = command.ShippingAddress,
                Items = command.Items
            };

            _mapperMock.Setup(m => m.Map<Shipment>(command)).Returns(shipment);
            _shipmentRepositoryMock.Setup(r => r.AddShipmentAsync(shipment)).Returns(Task.CompletedTask);
            _shipmentRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.AreEqual(shipment.ShipmentId, result);
            _mapperMock.Verify(m => m.Map<Shipment>(command), Times.Once);
            _shipmentRepositoryMock.Verify(r => r.AddShipmentAsync(shipment), Times.Once);
            _shipmentRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
