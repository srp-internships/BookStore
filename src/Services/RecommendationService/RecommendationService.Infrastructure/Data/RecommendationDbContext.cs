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
    internal class RecommendationDbContext : DbContext, IApplicationDbContext
    {
        public RecommendationDbContext(DbContextOptions<RecommendationDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<BookCategory> BookCategories { get; set; }


    }
}
