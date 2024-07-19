using OrderService.Domain.Entities;

namespace OrderService.IntegrationEvents;
public sealed record OrderProcessedIntegrationEvent(List<Customer> Customers, List<Book> Books);

