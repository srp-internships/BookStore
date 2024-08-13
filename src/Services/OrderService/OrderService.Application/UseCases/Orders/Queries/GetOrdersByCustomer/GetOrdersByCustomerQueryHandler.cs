using OrderService.Application.Common.Exceptions;

namespace OrderService.Application.UseCases.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerHandler
    : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersByCustomerHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetOrdersByCustomerIdAsNoTrackingAsync(query.CustomerId, cancellationToken);

        if (orders == null || !orders.Any())
        {
            throw new NotFoundException($"No orders found for customer with ID {query.CustomerId}");
        }

        return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
    }
}