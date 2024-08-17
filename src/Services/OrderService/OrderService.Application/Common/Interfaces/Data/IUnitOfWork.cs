namespace OrderService.Application.Common.Interfaces.Data;

public interface IUnitOfWork
{
    IOrderRepository OrderRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
