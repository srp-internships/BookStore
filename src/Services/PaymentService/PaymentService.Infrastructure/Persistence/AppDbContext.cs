using Microsoft.EntityFrameworkCore;
using PaymentService.Domain;
using PaymentService.Infrastructure.Persistence.Constants;

namespace PaymentService.Infrastructure.Persistence
{
    // add-migration Init -context AppDbContext -o Persistence/Migrations
    // update-database -context AppDbContext
    // migration -context AppDbContext
    // remove-migration -context AppDbContext
    // drop-database -context AppDbContext
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IUnitOfWork
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(Schemas.PaymentService);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
