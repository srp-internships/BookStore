using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);
        builder.Property(oi => oi.BookId);
        builder.Property(oi => oi.SellerId);
        builder.Property(oi => oi.Quantity);
        builder.Property(oi => oi.Price);
        builder.Property(oi => oi.Title);
    }
}
