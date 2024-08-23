using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CartService.Infrastructure.Persistence.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(ci => ci.Id);
            builder.HasOne<Book>()
                   .WithMany()
                   .HasForeignKey(ci => ci.BookId);
            builder.HasOne<Cart>()
                   .WithMany(c => c.Items)
                   .HasForeignKey(ci => ci.CartId);
        }
    }
}
