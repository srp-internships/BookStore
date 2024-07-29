using AutoMapper;
using Moq;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Create;
using ShipmentService.Aplication.Interfaces;
using ShipmentService.Domain.Entities.Shipments;

namespace ShipmentService.Aplication.UnitTests.NUnit.CQRS.Shipments.Commands.Create
{
    public class CreateShipmentCommandHandlerTests
    {
        private Mock<IShipmentRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private CreateShipmentCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IShipmentRepository>();
            _mockMapper = new Mock<IMapper>();

            _handler = new CreateShipmentCommandHandler(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Handle_Should_Create_Shipment_And_Return_ShipmentId()
        {
            // Arrange
            var command = new CreateShipmentCommand(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                new ShippingAddress { Id = Guid.NewGuid(), Street = "123 Main St", City = "Anytown", Country = "Country" },
                new List<ShipmentItem> { new ShipmentItem { ItemId = Guid.NewGuid(), BookName = "Some Book", Quantity = 1 } }
            );

            var shipment = new Shipment
            {
                ShipmentId = Guid.NewGuid(),
                OrderId = command.OrderId,
                CustomerId = command.CustomerId,
                ShippingAddress = command.ShippingAddress,
                Items = command.Items
            };

            _mockMapper.Setup(m => m.Map<Shipment>(command)).Returns(shipment);
            _mockRepository.Setup(r => r.AddShipmentAsync(shipment)).Returns(Task.CompletedTask);
            _mockRepository.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.AreEqual(shipment.ShipmentId, result);
            _mockMapper.Verify(m => m.Map<Shipment>(command), Times.Once);
            _mockRepository.Verify(r => r.AddShipmentAsync(shipment), Times.Once);
            _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
