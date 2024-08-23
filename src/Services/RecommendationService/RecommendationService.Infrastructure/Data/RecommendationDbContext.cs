using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecommendationService.Domain.Entities;
using RecommendationService.Application.Interfaces;

namespace RecommendationService.Infrastructure.Data
{
    public class RecommendationDbContext : DbContext, IApplicationDbContext
    {
        public RecommendationDbContext(DbContextOptions<RecommendationDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        //public DbSet<BookCategory> BookCategories { get; set; }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasMany(bc => bc.BookCategories)
                .WithMany(c => c.BookCategories);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }


    }
}
