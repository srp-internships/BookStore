namespace OrderService.Application.Common.Interfaces.Data;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<Book> Books { get; }
    DbSet<Order> Orders { get; }
    DbSet<OrderItem> OrderItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
