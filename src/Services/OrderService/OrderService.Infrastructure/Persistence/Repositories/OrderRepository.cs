using OrderService.Application.Common.Interfaces.Repositories;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DbSet<Order> _dbSet;

    public OrderRepository(ApplicationDbContext context)
    {
        _dbSet = context.Set<Order>();
    }

    public virtual async Task<Order> CreateAsync(Order order, CancellationToken token = default)
    {
        await _dbSet.AddAsync(order, token);

        return order;
    }

    public virtual async Task DeleteAsync(Guid orderId, CancellationToken token = default)
    {
        var order = await _dbSet.FindAsync(orderId);

        if (order != null)
            _dbSet.Remove(order);
    }

    public virtual async Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
               .Include(o => o.Items)
               .ToListAsync(cancellationToken);
    }

    public virtual async Task<Order?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public virtual async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsNoTrackingAsync(Guid CustomerId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
                       .Include(o => o.Items)
                       .AsNoTracking()
                       .Where(o => o.CustomerId == CustomerId)
                       .ToListAsync(cancellationToken);
    }
}

