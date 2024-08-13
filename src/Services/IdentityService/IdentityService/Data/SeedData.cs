using Duende.IdentityServer.EntityFramework.Mappers;
using IdentityService.Components.IDS;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data
{
	public static class SeedData
	{
		public static void EnsureSeedData(WebApplication app)
		{
			using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
			scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

			var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
			context.Database.Migrate();

			if (!context.Clients.Any())
			{
				foreach (var client in IdsConfig.Clients.ToList())
				{
					context.Clients.Add(client.ToEntity());
				}
				context.SaveChanges();
			}

			if (!context.IdentityResources.Any())
			{
				foreach (var resource in IdsConfig.IdentityResources.ToList())
				{
					context.IdentityResources.Add(resource.ToEntity());
				}
				context.SaveChanges();
			}

			if (!context.ApiScopes.Any())
			{
				foreach (var resource in IdsConfig.ApiScopes.ToList())
				{
					context.ApiScopes.Add(resource.ToEntity());
				}
				context.SaveChanges();
			}

			if (!context.ApiResources.Any())
			{
				foreach (var resource in IdsConfig.ApiResources.ToList())
				{
					context.ApiResources.Add(resource.ToEntity());
				}
				context.SaveChanges();
			}
		}
	}
}
