using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain;
using OrderService.Infrastructure.Persistence.Configurations;
using System.Reflection;
using OrderService.Application.Common.Interfaces.Data;

namespace OrderService.Infrastructure.Persistence.DataBases;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Ignore<BaseEntity>();    
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=OrderService;Username=postgres;Password=7878_Postgresql");
        base.OnConfiguring(optionsBuilder);
    }

}

