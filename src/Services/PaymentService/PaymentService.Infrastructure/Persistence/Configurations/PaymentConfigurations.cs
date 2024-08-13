using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentService.Domain.Entities.Payments;
using PaymentService.Infrastructure.Persistence.Constants;

namespace PaymentService.Infrastructure.Persistence.Configurations
{
	internal class PaymentConfigurations : IEntityTypeConfiguration<Payment>
	{
		public void Configure(EntityTypeBuilder<Payment> builder)
		{
			builder.ToTable(TableNames.Payments);

			builder.HasKey(i => i.Id);
			builder.Property(i => i.Id).ValueGeneratedNever();

			builder.Property(i => i.OrderId).IsRequired();
			builder.HasIndex(i => i.OrderId);

			builder.Property(i => i.RequestedAtUtc).IsRequired();

			builder.Property(i => i.ProcessedAtUtc).IsRequired(false);

			builder.HasMany(i => i.Transaction)
					.WithOne(i => i.Payment)
					.HasForeignKey(i => i.PaymentId)
					.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
