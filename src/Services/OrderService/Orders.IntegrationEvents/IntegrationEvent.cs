using OrderService.Application.Common.Interfaces;

namespace OrderService.IntegrationEvents;

public abstract record IntegrationEvent(Guid Id, DateTime OccurredOnUtc) : IIntegrationEvent;
