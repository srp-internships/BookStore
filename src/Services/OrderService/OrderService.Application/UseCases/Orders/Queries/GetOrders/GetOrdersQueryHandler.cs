using OrderService.Application.Common.Interfaces.Data;
using OrderService.Application.UseCases.DTOs;

namespace OrderService.Application.UseCases.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var allOrders = await _unitOfWork.OrderRepository.GetAllOrdersAsync(cancellationToken);
            var totalCount = allOrders.Count();

            var orders = allOrders
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToList();

            return new GetOrdersResult(
                new PaginatedResult<OrderDto>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    orders.ToOrderDtoList()));
        }
    }
}
