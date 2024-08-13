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

			builder.Entity<Role>()
				.HasData([
						new Role(Guid.Parse("EE5B756A-719D-4251-8293-3CF25BCD31F8"), Role.Admin),
						new Role(Guid.Parse("5635BCF7-D0B1-40AD-ABAE-09DC5090D4C0"), Role.Seller),
						new Role(Guid.Parse("62ADC034-2709-40DB-B198-170EFC076CFF"), Role.Customer)
					]);
		}
	}
}
