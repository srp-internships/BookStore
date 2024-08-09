using OrderService.Application.Common.Interfaces.Data;
using OrderService.Application.UseCases.DTOs;

namespace OrderService.Application.UseCases.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrdersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pagingParams = query.PagingParameters;

        var paginatedOrders = await _unitOfWork.OrderRepository
            .GetAllOrdersAsync(pagingParams, cancellationToken);

        var totalCount = paginatedOrders.Count();

        var ordersDto = paginatedOrders
            .Select(order => order.ToOrderDto())
            .ToList();

        var paginatedResult = new PaginatedResult<OrderDto>(
            pagingParams.PageNumber,
            pagingParams.PageSize,
            totalCount,
            ordersDto
        );

        return new GetOrdersResult(paginatedResult);
    }
}
