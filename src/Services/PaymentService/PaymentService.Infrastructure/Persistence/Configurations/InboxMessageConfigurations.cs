using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PaymentService.Infrastructure.Persistence.Constants;
using PaymentService.Application.Common.Inbox;

namespace PaymentService.Infrastructure.Persistence.Configurations
{
	internal class InboxMessageConfigurations : IEntityTypeConfiguration<InboxMessage>
	{
		public void Configure(EntityTypeBuilder<InboxMessage> builder)
		{
			builder.ToTable(TableNames.InboxMessages);

			builder.HasKey(i => i.Id);
			builder.Property(i => i.Id).ValueGeneratedNever();

			builder.Property(i => i.OccurredOnUtc).IsRequired();
			builder.Property(i => i.Type).IsRequired();
			builder.Property(i => i.Content).IsRequired();
			builder.Property(i => i.ProcessedOnUtc).IsRequired(false);
			builder.Property(i => i.Error).IsRequired(false);
		}
	}
}
