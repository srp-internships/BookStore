using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.Domain;
using PaymentService.Domain.Entities.Cards;
using PaymentService.Domain.Entities.Payments;
using PaymentService.Infrastructure.Persistence;
using PaymentService.Infrastructure.Persistence.Repositories;

namespace PaymentService.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			// Adding DataBase
			services.AddDbContext<AppDbContext>((serviceProvider, options) =>
			{
				string path = Directory.GetCurrentDirectory();
				var connectionString = configuration.GetSection("DataBase").Value;
				options.UseSqlServer(connectionString.Replace("[DataDirectory]", path));
			});

			// Adding Repository and Unit of work with Repository patterns implementations
			services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());
			services.AddScoped<ICardRepository, CardRepository>();
			services.AddScoped<IPaymentRepository, PaymentRepository>();

			return services;
		}
	}
}
