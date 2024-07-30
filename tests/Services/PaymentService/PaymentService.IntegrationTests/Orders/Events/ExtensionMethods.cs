using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.Common.Inbox;
using PaymentService.Domain.Entities.Cards;
using PaymentService.Domain.Entities.Payments;
using PaymentService.Domain;
using PaymentService.Infrastructure.Persistence;
using PaymentService.Infrastructure.Persistence.Repositories;
using MassTransit;
using PaymentService.Application.Orders.Events;

namespace PaymentService.IntegrationTests.Orders.Events
{
    internal static class ExtensionMethods
    {
        public static ServiceCollection CreateServiceProvider()
        {
            var services = new ServiceCollection();
            return services;
        }

        public static ServiceCollection RegisterInMemoryDbContext(this ServiceCollection services)
        {
            // Adding InMemory EF Core
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));

            // Adding Repository and Unit of work with Repository patterns implementations
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IInboxRepository, InboxRepository>();

            return services;
        }

        public static ServiceCollection RegisterMassTransit(this ServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderProcessedIntegrationEventConsumer>();

                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddSingleton<OrderTestIntegrationEventPublisher>();

            services.AddMassTransitTestHarness();

            return services;
        }

        public static async Task<Card> AddCustomer(this ServiceProvider serviceProvider)
        {
            var cardRepository = serviceProvider.GetRequiredService<ICardRepository>();
            var db = serviceProvider.GetRequiredService<IUnitOfWork>();

            var customer = new Card
            {
                Id = Guid.NewGuid(),
                IsDeleted = false,
                UserId = Guid.NewGuid(),
                CardExpirationDate = new DateOnly(2029, 1, 1),
                CardNumber = "4444333322221111",
                CardHolderName = "Tom",
                CardCvc = "123",
                CardHolderRole = CardHolderRole.Customer,
            };

            cardRepository.Create(customer);

            await db.SaveChangesAsync();

            return customer;
        }

        public static async Task<Card> AddSeller(this ServiceProvider serviceProvider)
        {
            var cardRepository = serviceProvider.GetRequiredService<ICardRepository>();
            var db = serviceProvider.GetRequiredService<IUnitOfWork>();

            var seller = new Card
            {
                Id = Guid.NewGuid(),
                IsDeleted = false,
                UserId = Guid.NewGuid(),
                CardExpirationDate = new DateOnly(2029, 1, 1),
                CardNumber = "4444333322221111",
                CardHolderName = "Tom",
                CardCvc = "123",
                CardHolderRole = CardHolderRole.Customer,
            };

            cardRepository.Create(seller);

            await db.SaveChangesAsync();

            return seller;
        }
    }
}