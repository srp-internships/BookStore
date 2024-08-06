namespace OrderService.Application.Common.Interfaces.Repositories;

public interface IGetOrdersByCustomerRepository : IBaseRepository<Order>
{
    Task<List<Order>> GetOrdersByCustomerAsync(Guid CustomerId, CancellationToken cancellationToken = default);
}
