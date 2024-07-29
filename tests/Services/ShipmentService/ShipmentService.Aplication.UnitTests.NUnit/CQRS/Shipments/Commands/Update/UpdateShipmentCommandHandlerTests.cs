using AutoMapper;
using Moq;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Update;
using ShipmentService.Aplication.Interfaces;
using ShipmentService.Domain.Entities.Shipments;
using ShipmentService.Domain.Enums;
using ShipmentService.IntegrationEvent;
using ShipmentService.Aplication.Common.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MediatR;

namespace ShipmentService.Aplication.UnitTests.NUnit.CQRS.Shipments.Commands.Update
{
    [TestFixture]
    public class UpdateShipmentCommandHandlerTests
    {
        private Mock<IShipmentRepository> _shipmentRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IPublishEndpoint> _publishEndpointMock;
        private UpdateShipmentCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _shipmentRepositoryMock = new Mock<IShipmentRepository>();
            _mapperMock = new Mock<IMapper>();
            _publishEndpointMock = new Mock<IPublishEndpoint>();
            _handler = new UpdateShipmentCommandHandler(_shipmentRepositoryMock.Object, _mapperMock.Object, _publishEndpointMock.Object);
        }

        [Test]
        public void Handle_ShipmentNotFound_ThrowsException()
        {
            // Arrange
            var command = new UpdateShipmentCommand(
                Guid.NewGuid(),
                Guid.NewGuid(),
                ShipmentStatus.Pending, // Assuming ShipmentStatus is an enum
                DateTime.Now,
                "Shipment status update"
            );

            _shipmentRepositoryMock.Setup(repo => repo.GetShipmentByIdAsync(command.ShipmentId))
                                   .ReturnsAsync((Shipment)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("Shipment not found"));
        }

        [Test]
        public async Task Handle_ValidCommand_UpdatesShipment()
        {
            // Arrange
            var shipmentId = Guid.NewGuid();
            var orderId = Guid.NewGuid();
            var shipment = new Shipment
            {
                ShipmentId = shipmentId,
                OrderId = orderId,
                Status = ShipmentService.Domain.Enums.Status.Pending
            };

            var command = new UpdateShipmentCommand(
                ShipmentId: shipmentId,
                OrderId: orderId,
                Status: ShipmentStatus.Shipped,
                UpdatedStatusDateTime: DateTime.Now,
                Message: "Shipment status updated"
            );

            _shipmentRepositoryMock.Setup(repo => repo.GetShipmentByIdAsync(shipmentId)).ReturnsAsync(shipment);
            _mapperMock.Setup(mapper => mapper.Map(command, shipment)).Callback(() => {
                shipment.Status = ShipmentService.Domain.Enums.Status.Shipped;
            });

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _shipmentRepositoryMock.Verify(repo => repo.UpdateShipmentAsync(shipment), Times.Once);
            _shipmentRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
            _publishEndpointMock.Verify(endpoint => endpoint.Publish(It.Is<ShipmentUpdatedEvent>(e =>
                e.ShipmentId == shipmentId &&
                e.OrderId == orderId &&
                e.Status == ShipmentStatus.Shipped &&
                e.Message == command.Message
            ), It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
