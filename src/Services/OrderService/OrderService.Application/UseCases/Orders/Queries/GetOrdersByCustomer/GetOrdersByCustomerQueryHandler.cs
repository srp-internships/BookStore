namespace OrderService.Application.UseCases.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerHandler
    : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersByCustomerHandler(IOrderRepository getOrdersByCustomerRepository)
    {
        _orderRepository = getOrdersByCustomerRepository;
    }

    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetOrdersByCustomerIdAsNoTrackingAsync(query.CustomerId, cancellationToken);

        return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
    }
}
