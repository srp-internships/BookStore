using Duende.IdentityServer.EntityFramework.DbContexts;
using IdentityService.Data.Constants;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data
{
	// Add-Migration Init -Context ConfigurationDbContext -OutputDir Data/Migrations/ConfigurationDb
	// update-database -Context ConfigurationDbContext
	// remove-migration -context "ConfigurationDbContext"
	public class ConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options) 
		: ConfigurationDbContext<ConfigurationDbContext>(options)
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(Schemas.IdsConfigurationSchema);

			base.OnModelCreating(modelBuilder);
		}
	}
}
