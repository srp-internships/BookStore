using OrderService.Application.Common.Interfaces.Data;
using OrderService.Application.Common.Interfaces.Repositories;

namespace OrderService.Infrastructure.Persistence
{
    public class UnitOfWork(ApplicationDbContext context, IOrderRepository orderRepository) : IUnitOfWork
    {
        public IOrderRepository OrderRepository => orderRepository;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
