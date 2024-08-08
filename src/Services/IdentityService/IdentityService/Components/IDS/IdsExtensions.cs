using Duende.IdentityServer.AspNetIdentity;
using IdentityService.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdentityService.Components.IDS
{
	public static class IdsExtensions
	{
		public static IServiceCollection AddIds(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddIdentityServer(options =>
			{
				options.Events.RaiseErrorEvents = true;
				options.Events.RaiseInformationEvents = true;
				options.Events.RaiseFailureEvents = true;
				options.Events.RaiseSuccessEvents = true;

				options.UserInteraction.LoginUrl = "/Account/Login";
				options.UserInteraction.LogoutUrl = "/Account/Logout";
				options.UserInteraction.ErrorUrl = "/Error";
			})
				.AddJwtBearerClientAuthentication()
				.AddAspNetIdentity<User>()

				// Configuring IdentityServer4 to use EF Core + MS Sql Server as storage (instead of InMemory)
				// See https://identityserver4.readthedocs.io/en/latest/quickstarts/5_entityframework.html
				.AddConfigurationStore<ConfigurationDbContext>(options =>
				{
					options.ConfigureDbContext = b => b.UseSqlServer(configuration.GetConnectionString("IdsDb").Replace("[DataDirectory]", Directory.GetCurrentDirectory()),
						sql => sql.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name));
				})
				.AddOperationalStore<PersistedGrantDbContext>(options =>
				{
					options.ConfigureDbContext = b => b.UseSqlServer(configuration.GetConnectionString("IdsDb").Replace("[DataDirectory]", Directory.GetCurrentDirectory()),
						sql => sql.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name));
				})
				//.AddSigningCredential(certificate);                                    // Adding real certificate
				.AddDeveloperSigningCredential();                                        // TODO __##__ Adding test certificate (WARNING USE ONLY FOR DEVELOPMENT)

			return services;
		}
	}
}
