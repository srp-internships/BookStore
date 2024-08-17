using ShipmentService.Aplication.Interfaces;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipmentService.Aplication.CQRS.Shipments.Queries.GetById;
using System.Runtime.InteropServices.Marshalling;
using ShipmentService.Domain.Entities.Shipments;

namespace ShipmentService.Aplication.UnitTests.NUnit.CQRS.Shipments.Queries.GetByIds
{
    [TestFixture]
    public class GetShipmentByIdQueryHandlerTests
    {
        private Mock<IUnitOfWork>? _unitOfWork;
        private GetShipmentByIdQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _handler = new GetShipmentByIdQueryHandler(_unitOfWork.Object);
        }
        [Test]
        public async Task Handle_ShouldReturnShipment_WhenFound()
        {
            //Arrage
            var shipmentId= Guid.NewGuid();
            var shipment = new Shipment
            {
                ShipmentId = shipmentId,

            };
            _unitOfWork
                .Setup(r => r.Shipments.GetShipmentByIdAsync(shipmentId))
                .ReturnsAsync(shipment);
            var query=new GetShipmentByIdQuery(shipmentId);

            //Act
            var result= await _handler.Handle(query,CancellationToken.None);

            //Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(shipmentId, result.ShipmentId); 
        }
        [Test]
        public async Task Handle_ShouldReturnNull_WhenNotFound()
        {
            //Arrage
            var  shipmentId=Guid.NewGuid();
            _unitOfWork
                .Setup(r => r.Shipments.GetShipmentByIdAsync(shipmentId))
                .ReturnsAsync((Shipment)null);

            var query =new GetShipmentByIdQuery(shipmentId);

            //Act
            var result= await _handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.IsNull(result);
        }
    }
}
