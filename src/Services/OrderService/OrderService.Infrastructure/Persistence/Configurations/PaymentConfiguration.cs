using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;

namespace OrderService.Infrastructure.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.PaymentStatus)
                  .HasConversion(
                      p => p.ToString(),
                      p => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), p))
                  .IsRequired()
                  .HasMaxLength(50);
        builder.Property(p => p.Message).IsRequired();
    }
}
