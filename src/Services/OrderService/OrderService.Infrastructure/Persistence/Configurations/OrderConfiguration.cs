using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;


namespace OrderService.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.CartId);

        builder.HasOne<Customer>(i => i.Customer)
          .WithMany(o => o.Orders)
          .HasForeignKey(o => o.CustomerId)
          .IsRequired();

        builder.HasMany(o => o.Items)
            .WithOne(o => o.Order)
            .HasForeignKey(oi => oi.OrderId);

        builder.ComplexProperty(
           o => o.ShippingAddress, addressBuilder =>
           {
               addressBuilder.Property(a => a.FirstName)
                   .HasMaxLength(50);

               addressBuilder.Property(a => a.LastName)
                    .HasMaxLength(50);

               addressBuilder.Property(a => a.EmailAddress)
                   .HasMaxLength(50);

               addressBuilder.Property(a => a.Country)
                   .HasMaxLength(50);

               addressBuilder.Property(a => a.State)
                   .HasMaxLength(50);

               addressBuilder.Property(a => a.Street)
                   .HasMaxLength(50);

           });

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.PaymentProcessing)
            .HasConversion(
                s => s.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

        builder.Property(o => o.TotalPrice);
    }
}
