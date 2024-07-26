using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OrderService.Application.Common.Interfaces.Data;
using OrderService.Domain.Entities;

namespace OrderService.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerHandler : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMemoryCache _cache;

    public GetOrdersByCustomerHandler(IApplicationDbContext dbContext, IMemoryCache cache)
    {
        _dbContext = dbContext;
        _cache = cache;
    }

    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var cacheKey = $"CustomerOrders_{query.CustomerId}";

        if (!_cache.TryGetValue(cacheKey, out List<Order> orders))
        {
            orders = await _dbContext.Orders
                        .Include(o => o.Items)
                        .AsNoTracking()
                        .Where(o => o.CustomerId == query.CustomerId)
                        .ToListAsync(cancellationToken);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromDays(1)); 
            _cache.Set(cacheKey, orders, cacheEntryOptions);
        }

        return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
    }
}
