using OrderService.Application.Common.Interfaces.Repositories;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Persistence.Repositories;

public class GetOrdersByCustomerRepository : IGetOrdersByCustomerRepository
{
    private readonly ApplicationDbContext _context;
    public GetOrdersByCustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Order> CreateAsync(Order entity, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Order entity, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Order>> GetOrdersByCustomerAsync(Guid CustomerId,
        CancellationToken cancellationToken = default)
    {
        var orders = await _context.Orders
                       .Include(o => o.Items)
                       .AsNoTracking()
                       .Where(o => o.CustomerId == CustomerId)
                       .ToListAsync(cancellationToken);
        return orders;
    }

    public Task<Order> UpdateAsync(Order entity, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
