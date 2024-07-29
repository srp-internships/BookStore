using System;
using System.Collections.Generic;
namespace OrderService.IntegrationEvents;

public record OrderStatusUpdatedIntegrationEvent(Guid Id, string Status);
