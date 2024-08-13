using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Common.Interfaces.Data;
using OrderService.Application.Common.Interfaces.Repositories;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using OrderService.Infrastructure.Consumers;
using OrderService.Infrastructure.Persistence;
using OrderService.Infrastructure.Persistence.DataBases;
using OrderService.Infrastructure.Persistence.Repositories;
using OrderService.IntegrationTests.Payments;
using OrderService.IntegrationTests.Shipments;


namespace OrderService.IntegrationTests;

public static class ExtensionMethods
{
    public static ServiceCollection CreateServiceProvider()
    {
        var services = new ServiceCollection();
        return services;
    }

    public static ServiceCollection RegisterInMemoryDbContext(this ServiceCollection services)
    {
        // Adding InMemory EF Core
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("TestDatabase"));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
    public static ServiceCollection RegisterMassTransit(this ServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<PaymentStatusUpdatedConsumer>();
            x.AddConsumer<ShipmentStatusUpdatedConsumer>();

            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddSingleton<PaymentRequestProcessedEventPublisher>();
        services.AddSingleton<ShipmentUpdatedEventPublisher>();

        services.AddMassTransitTestHarness();

        return services;
    }

    public static async Task<Order> AddOrder(this ServiceProvider serviceProvider)
    {
        var orderRepository = serviceProvider.GetRequiredService<IOrderRepository>();
        var db = serviceProvider.GetRequiredService<IUnitOfWork>();

        var order = new Order
        {
            Id = Guid.NewGuid(),
            Status = OrderStatus.PaymentProcessing,
            CustomerId = Guid.NewGuid(),
            ShippingAddress = new Address
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                Country = "USA",
                State = "NY",
                Street = "456 Elm St"
            },
            Items = new List<OrderItem>
    {
        new OrderItem
        {
            Id = Guid.NewGuid(),
            Quantity = 3,
            Price = 29.99m,
            BookId = Guid.NewGuid(),
            SellerId = Guid.NewGuid(),
            Title = "The Great Book"
        },
        new OrderItem
        {
            Id = Guid.NewGuid(),
            Quantity = 1,
            Price = 15.99m,
            BookId = Guid.NewGuid(),
            SellerId = Guid.NewGuid(),
            Title = "Another Great Book"
        }
    }
        };

        await orderRepository.CreateAsync(order);

        await db.SaveChangesAsync();

        return order;
    }
}