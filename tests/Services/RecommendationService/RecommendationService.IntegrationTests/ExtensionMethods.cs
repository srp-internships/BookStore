using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecommendationService.Application.Consumers;
using RecommendationService.Application.Interfaces;
using RecommendationService.Infrastructure.Data;

namespace RecommendationService.IntegrationTests
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
			services.AddDbContext<RecommendationDbContext>(options =>
				options.UseInMemoryDatabase("TestDatabase"));

			// Adding Repository and Unit of work with Repository patterns implementations
			services.AddScoped<IApplicationDbContext, RecommendationDbContext>();

			return services;
		}

		public static ServiceCollection RegisterMassTransit(this ServiceCollection services)
		{
			services.AddMassTransit(x =>
			{
				x.AddConsumer<BookCreatedConsumer>();
				x.AddConsumer<BookUpdatedConsumer>();
				x.AddConsumer<CategoryCreatedConsumer>();
				x.AddConsumer<CategoryUpdatedConsumer>();

				x.UsingInMemory((context, cfg) =>
				{
					cfg.ConfigureEndpoints(context);
				});
			});

			services.AddSingleton<CatalogTestIntegrationEventPublisher>();

			services.AddMassTransitTestHarness();

			return services;
		}
	}
}
