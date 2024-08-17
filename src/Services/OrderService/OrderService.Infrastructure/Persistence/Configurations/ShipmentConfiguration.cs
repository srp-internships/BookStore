using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;

namespace OrderService.Infrastructure.Persistence.Configurations;

public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
{
    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(e => e.ShipmentStatus)
                  .HasConversion(
                      v => v.ToString(),
                      v => (ShipmentStatus)Enum.Parse(typeof(ShipmentStatus), v))
                  .IsRequired()
                  .HasMaxLength(50);
        builder.Property(x => x.Message).IsRequired(false);
    }
}
