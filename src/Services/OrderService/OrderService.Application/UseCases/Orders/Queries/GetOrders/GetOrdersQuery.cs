using OrderService.Application.UseCases.DTOs;

namespace OrderService.Application.UseCases.Orders.Queries.GetOrders;

public record GetOrdersQuery(PagingParameters PagingParameters)
    : IQuery<GetOrdersResult>;

public record GetOrdersResult(PaginatedResult<OrderDto> Orders);
