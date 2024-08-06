using OrderService.Application.Common.Interfaces.Data;
using OrderService.Application.Common.Interfaces.Repositories;
using OrderService.Infrastructure.Persistence.Repositories;

namespace OrderService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        // Add services to the container.
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IGetOrdersByCustomerRepository, GetOrdersByCustomerRepository>();
        services.Decorate<IGetOrdersByCustomerRepository, CachedGetOrdersByCustomerRepository>();

        return services;
    }
}
