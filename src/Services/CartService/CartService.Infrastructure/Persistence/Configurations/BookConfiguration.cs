using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CartService.Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(b => b.Image)
                .HasMaxLength(500)
                .IsRequired(false);
        }
    }
}
