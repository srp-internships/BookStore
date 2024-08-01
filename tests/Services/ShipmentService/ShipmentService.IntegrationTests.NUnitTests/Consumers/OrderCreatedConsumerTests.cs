using Moq;
using MassTransit;
using Microsoft.Extensions.Logging;
using ShipmentService.Infrastructure.Consumers;
using ShipmentService.Infrastructure.Persistence.DbContexts;
using ShipmentService.Domain.Entities.Shipments;
using Microsoft.EntityFrameworkCore;
using OrderService.IntegrationEvents;
using ShipmentService.Domain.Enums;
using MassTransit.Testing;

namespace ShipmentService.Infrastructure.IntegrationTests.NUnitTests.Consumers
{
    public class OrderCreatedConsumerTests
    {
      
    }
}
