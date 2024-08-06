namespace OrderService.Application.UseCases.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerHandler : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    private readonly IGetOrdersByCustomerRepository _getOrdersByCustomerRepository;

    public GetOrdersByCustomerHandler(IGetOrdersByCustomerRepository getOrdersByCustomerRepository)
    {
        _getOrdersByCustomerRepository = getOrdersByCustomerRepository;
    }

    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await _getOrdersByCustomerRepository.GetOrdersByCustomerAsync(query.CustomerId, cancellationToken);
        return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
    }
}
