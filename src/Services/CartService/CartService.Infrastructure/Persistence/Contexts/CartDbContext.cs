using CartService.Infrastructure.Persistence.Configurations;

namespace CartService.Infrastructure.Persistence.Contexts
{
    public class CartDbContext : DbContext
    {
        public CartDbContext(DbContextOptions<CartDbContext> options)
       : base(options) { }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> Items { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookSeller> BookSellers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new BookSellerConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
