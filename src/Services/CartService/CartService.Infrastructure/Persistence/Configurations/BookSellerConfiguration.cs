using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CartService.Infrastructure.Persistence.Configurations
{
    public class BookSellerConfiguration : IEntityTypeConfiguration<BookSeller>
    {
        public void Configure(EntityTypeBuilder<BookSeller> builder)
        {
            builder.HasKey(bs => new { bs.BookId, bs.SellerId });
        }
    }
}
