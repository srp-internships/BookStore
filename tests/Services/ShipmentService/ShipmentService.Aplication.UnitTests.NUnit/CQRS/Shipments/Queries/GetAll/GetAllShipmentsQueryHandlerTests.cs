using Moq;
using ShipmentService.Aplication.CQRS.Shipments.Queries.GetAll;
using ShipmentService.Aplication.Interfaces;
using ShipmentService.Domain.Entities.Shipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.UnitTests.NUnit.CQRS.Shipments.Queries.GetAll
{
    [TestFixture]
    public class GetAllShipmentsQueryHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private GetAllShipmentsQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _handler = new GetAllShipmentsQueryHandler(_unitOfWork.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnAllShipmentsSuccessfully()
        {
            // Arrange
            var shipments = new List<Shipment>
            {
                new Shipment { ShipmentId = Guid.NewGuid() },
                new Shipment { ShipmentId = Guid.NewGuid() }
            };

            _unitOfWork
                .Setup(r => r.Shipments.GetAllShipmentsAsync())
                .ReturnsAsync(shipments);

            var query = new GetShipmentsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(shipments.Count, result.Count());
            CollectionAssert.AreEqual(shipments, result.ToList());
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyList_WhenNoShipmentsFound()
        {
            // Arrange
            _unitOfWork
                .Setup(r => r.Shipments.GetAllShipmentsAsync())
                .ReturnsAsync(new List<Shipment>());

            var query = new GetShipmentsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }
    }
}
