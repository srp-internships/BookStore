using AutoMapper;
using ShipmentService.Aplication.Common.Mappings;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Update;
using ShipmentService.Aplication.CQRS.Shipments.Queries.GetAll;
using ShipmentService.Aplication.CQRS.Shipments.Queries.GetById;
using ShipmentService.Domain.Entities.Shipments;
using ShipmentService.Domain.Enums;
using ShipmentService.IntegrationEvent;

namespace ShipmentService.Aplication.UnitTests.NUnit.Common.Mappings
{
    [TestFixture]
    public class ShipmentMappingsTests
    {
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ShipmentMappings>(); 
            });

            _mapper = configuration.CreateMapper();
        }

        [Test]
        public void Should_Map_Shipment_To_GetShipmentByIdQuery()
        {
            // Arrange
            var shipment = new Shipment
            {
                ShipmentId = Guid.NewGuid(),
                OrderId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                ShippingAddress = new ShippingAddress { Id = Guid.NewGuid(), Street = "123 Main St", City = "Springfield", Country = "USA" },
                Items = new List<ShipmentItem> { new ShipmentItem { ItemId = Guid.NewGuid(), BookName = "Book Title", Quantity = 1 } },
                Status = Status.Shipped,
                UpdateShipmentStatus = DateTime.UtcNow,
                OrderStatus = OrderStatus.ShipmentProcessing
            };

            // Act
            var query = _mapper.Map<GetShipmentByIdQuery>(shipment);

            // Assert
            Assert.NotNull(query);
            Assert.AreEqual(shipment.ShipmentId, query.ShipmentId);
        }

        [Test]
        public void Should_Map_Shipment_To_GetShipmentsQuery()
        {
            // Arrange
            var shipments = new List<Shipment>
            {
                new Shipment
                {
                    ShipmentId = Guid.NewGuid(),
                    OrderId = Guid.NewGuid(),
                    CustomerId = Guid.NewGuid(),
                    ShippingAddress = new ShippingAddress { Id = Guid.NewGuid(), Street = "123 Main St", City = "Springfield", Country = "USA" },
                    Items = new List<ShipmentItem> { new ShipmentItem { ItemId = Guid.NewGuid(), BookName = "Book Title", Quantity = 1 } },
                    Status = Status.Shipped,
                    UpdateShipmentStatus = DateTime.UtcNow,
                    OrderStatus = OrderStatus.ShipmentProcessing
                }
            };

            // Act
            var query = _mapper.Map<IEnumerable<Shipment>>(shipments);

            // Assert
            Assert.NotNull(query);
            Assert.AreEqual(shipments.Count, query.Count());
            Assert.IsTrue(shipments.All(shipment => query.Any(q => q.ShipmentId == shipment.ShipmentId)));
        }

        [Test]
        public void Should_Map_GetShipmentByIdQuery_To_Shipment()
        {
            // Arrange
            var query = new GetShipmentByIdQuery(Guid.NewGuid());

            // Act
            var shipment = _mapper.Map<Shipment>(query);

            // Assert
            Assert.NotNull(shipment);
            Assert.AreEqual(query.ShipmentId, shipment.ShipmentId);
        }
    }
}
    

