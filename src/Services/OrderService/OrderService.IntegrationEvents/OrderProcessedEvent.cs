using OrderService.Domain.Abstractions;
using OrderService.Domain.Entities;

namespace OrderService.IntegrationEvents;

public sealed class OrderCreatedEvent(Order order) : IDomainEvent;


