using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentService.Domain.Entities.Payments;
using PaymentService.Infrastructure.Persistence.Constants;

namespace PaymentService.Infrastructure.Persistence.Configurations
{
	internal class TransactionConfigurations : IEntityTypeConfiguration<Transaction>
	{
		public void Configure(EntityTypeBuilder<Transaction> builder)
		{
			builder.ToTable(TableNames.Transactions);

			builder.HasKey(i => i.Id);
			builder.Property(i => i.Id).ValueGeneratedNever();

			builder.Property(i => i.Amount).IsRequired().HasPrecision(18, 2);

			builder.Property(i => i.Type).IsRequired();
		}
	}
}
