using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CartService.Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);
        }
    }
}
