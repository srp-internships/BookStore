namespace OrderService.Application.Common.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<Order> CreateAsync(Order order, CancellationToken token = default);
    Task DeleteAsync(Guid orderId, CancellationToken token = default);
    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsNoTrackingAsync(Guid CustomerId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default);
}

