using OrderService.Application.UseCases.DTOs;

namespace OrderService.Application.UseCases.Orders.Queries.GetOrdersByCustomer;
public record GetOrdersByCustomerQuery(Guid CustomerId)
    : IQuery<GetOrdersByCustomerResult>;

public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);
