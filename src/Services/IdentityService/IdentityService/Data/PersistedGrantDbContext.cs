using Duende.IdentityServer.EntityFramework.DbContexts;
using IdentityService.Data.Constants;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data
{
	// Add-Migration Init -Context PersistedGrantDbContext -OutputDir Data/Migrations/PersistedGrantDb
	// update-database -Context PersistedGrantDbContext
	// remove-migration -context "PersistedGrantDbContext"
	public class PersistedGrantDbContext(DbContextOptions<PersistedGrantDbContext> options) 
		: PersistedGrantDbContext<PersistedGrantDbContext>(options)
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(Schemas.IdsPersistedGrant);

			base.OnModelCreating(modelBuilder);
		}
	}
}
