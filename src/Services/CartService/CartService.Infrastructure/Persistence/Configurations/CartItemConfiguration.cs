using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CartService.Infrastructure.Persistence.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.BookId)
                .IsRequired();

            builder.Property(ci => ci.BookName)
                .HasMaxLength(200);

            builder.Property(ci => ci.ImageUrl)
                .HasMaxLength(500);

            builder.Property(ci => ci.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(ci => ci.Quantity)
                .IsRequired();

            builder.Property(ci => ci.SellerId)
                .IsRequired();
        }
    }
}
