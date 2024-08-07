using IdentityService.Data.Constants;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data
{
	// add-migration Init -context ApplicationDbContext -o Data/Migrations/ApplicationDbMigrations
	// update-database -context ApplicationDbContext
	// migration -context ApplicationDbContext
	// remove-migration -context ApplicationDbContext
	// drop-database -context ApplicationDbContext
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
		: IdentityDbContext<User, Role, Guid>(options)
	{
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.HasDefaultSchema(Schemas.IdentityProviderSchema);

			base.OnModelCreating(builder);
		}
	}
}
