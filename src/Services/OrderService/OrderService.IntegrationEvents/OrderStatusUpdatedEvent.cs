using OrderService.Domain.Abstractions;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.IntegrationEvents;

public sealed class OrderUpdatedEvent(Order order) : IDomainEvent;