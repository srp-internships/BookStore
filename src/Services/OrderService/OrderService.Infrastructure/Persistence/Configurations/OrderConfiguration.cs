using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.OrderDate)
                   .IsRequired();

            builder.Property(o => o.Status)
                   .IsRequired();

            builder.HasMany(o => o.Items)
                   .WithOne()
                   .HasForeignKey(oi => oi.Id)
                   .IsRequired();

            builder.HasOne(o => o.Payment)
                   .WithOne()
                   .HasForeignKey<Order>(o => o.PaymentId)
                   .IsRequired();

            builder.HasOne(o => o.Shipment)
                   .WithOne()
                   .HasForeignKey<Order>(o => o.ShipmentId)
                   .IsRequired();

            builder.HasOne(o => o.Customer)
                   .WithMany()
                   .HasForeignKey(o => o.CustomerId)
                   .IsRequired();

            builder.HasOne(o => o.Cart)
                   .WithMany()
                   .HasForeignKey(o => o.CartId)
                   .IsRequired();
        }
    }


