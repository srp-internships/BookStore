using OrderService.Application.Common.Interfaces.Repositories;
using OrderService.Application.Common.Pagination;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Persistence.Repositories;

public class DecoratedOrderRepository : IOrderRepository
{
    private readonly IDistributedCache distributedCache;
    private readonly IOrderRepository orderRepository;

    public DecoratedOrderRepository(
        IDistributedCache distributedCache,
        IOrderRepository orderRepository)
    {
        this.distributedCache = distributedCache;
        this.orderRepository = orderRepository;
    }

    public async Task<Order> CreateAsync(Order order, CancellationToken token = default)
    {
        var cacheKey = $"CustomerOrders_{order.CustomerId}";
        await distributedCache.RemoveAsync(cacheKey, token);
        return await orderRepository.CreateAsync(order, token);
    }

    public Task DeleteAsync(Order order, CancellationToken token = default)
    {
        return orderRepository.DeleteAsync(order, token);
    }

    public Task<IEnumerable<Order>> GetAllOrdersAsync(PagingParameters pagingParameters, CancellationToken cancellationToken = default)
    {
        return orderRepository.GetAllOrdersAsync(pagingParameters, cancellationToken);
    }

    public Task<Order?> GetAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        return orderRepository.GetAsync(orderId, cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsNoTrackingAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"CustomerOrders_{customerId}";
        var cachedData = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

        if (cachedData != null)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Order>>(cachedData, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }) ?? new List<Order>();
        }

        var orders = await orderRepository.GetOrdersByCustomerIdAsNoTrackingAsync(customerId, cancellationToken);

        var cacheEntryOptions = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromDays(1));

        var serializedData = JsonConvert.SerializeObject(orders, new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
        await distributedCache.SetStringAsync(cacheKey, serializedData, cacheEntryOptions, cancellationToken);

        return orders;
    }
}
