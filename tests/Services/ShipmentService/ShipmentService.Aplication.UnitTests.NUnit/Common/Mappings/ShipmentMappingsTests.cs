using AutoMapper;
using ShipmentService.Aplication.Common.Mappings;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Create;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Update;
using ShipmentService.Aplication.CQRS.Shipments.Queries.GetAll;
using ShipmentService.Aplication.CQRS.Shipments.Queries.GetById;
using ShipmentService.Domain.Entities.Shipments;
using ShipmentService.Domain.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.UnitTests.NUnit.Common.Mappings
{
    [TestFixture]
    public class ShipmentMappingsTests
    {
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ShipmentMappings>(); 
            });

            _mapper = config.CreateMapper();
        }

        [Test]
        public void Should_Map_Shipment_To_CreateShipmentCommand()
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
                    City = "Anytown",
                    Country = "Country"
                },
                Items = new List<ShipmentItem>
        {
            new ShipmentItem
            {
                ItemId = Guid.NewGuid(),
                BookName = "Some Book",
                Quantity = 1
            }
        }
            };

            // Act
            var command = _mapper.Map<CreateShipmentCommand>(shipment);

            // Assert
            Assert.AreEqual(shipment.ShipmentId, command.ShipmentId);
            Assert.AreEqual(shipment.OrderId, command.OrderId);
            Assert.AreEqual(shipment.CustomerId, command.CustomerId);
            Assert.AreEqual(shipment.ShippingAddress?.Street, command.ShippingAddress?.Street);
            Assert.AreEqual(shipment.ShippingAddress?.City, command.ShippingAddress?.City);
            Assert.AreEqual(shipment.ShippingAddress?.Country, command.ShippingAddress?.Country);
            Assert.AreEqual(shipment.Items.Count, command.Items.Count);
            Assert.AreEqual(shipment.Items.First().ItemId, command.Items.First().ItemId);
            Assert.AreEqual(shipment.Items.First().BookName, command.Items.First().BookName);
            Assert.AreEqual(shipment.Items.First().Quantity, command.Items.First().Quantity);
        }

    }
}
    

