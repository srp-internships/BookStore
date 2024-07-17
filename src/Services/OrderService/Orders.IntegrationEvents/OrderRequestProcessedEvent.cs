﻿namespace Orders.IntegrationEvents;

public sealed record OrderRequestProcessedEvent(
    Guid OrderId,
    Guid CustomerId,
    Guid SellerId,
    decimal TotalAmount
    );