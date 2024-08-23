using OrderService.Application.Common.Interfaces.Repositories;
using OrderService.Application.Common.Pagination;
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

    public virtual async Task DeleteAsync(Order order, CancellationToken token = default)
    {
        _dbSet.Remove(order);
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync(PagingParameters pagingParameters, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.Include(o => o.Items).AsQueryable();

        if (pagingParameters.OrderByDescending == true)
        {
            query = query.OrderByDescending(o => o.Id);
        }
        else
        {
            query = query.OrderBy(o => o.Id);
        }
        return await query
            .Skip(pagingParameters.Skip)
            .Take(pagingParameters.Take)
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

