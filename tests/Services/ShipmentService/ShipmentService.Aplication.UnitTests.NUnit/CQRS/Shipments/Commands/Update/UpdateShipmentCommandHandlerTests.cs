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
    }
}
