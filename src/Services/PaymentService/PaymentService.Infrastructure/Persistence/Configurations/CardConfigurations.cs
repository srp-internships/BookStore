using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentService.Domain.Entities.Cards;
using PaymentService.Infrastructure.Persistence.Constants;

namespace PaymentService.Infrastructure.Persistence.Configurations
{
	internal class CardConfigurations : IEntityTypeConfiguration<Card>
	{
		public void Configure(EntityTypeBuilder<Card> builder)
		{
			builder.ToTable(TableNames.Cards);

			builder.HasKey(i => i.Id);
			builder.Property(i => i.Id).ValueGeneratedNever();

			builder.Property(book => book.IsDeleted).IsRequired().HasDefaultValue(false);
			builder.HasQueryFilter(book => book.IsDeleted == false);

			builder.Property(i => i.UserId).IsRequired();
			builder.HasIndex(i => i.UserId);

			builder.Property(i => i.CardHolderRole).IsRequired();

			builder.Property(i => i.CardNumber).IsRequired();

			builder.Property(i => i.CardCvc).IsRequired();

			builder.Property(i => i.CardExpirationDate).IsRequired();

			builder.Property(i => i.CardHolderName).IsRequired();

			builder.HasMany(i => i.Transactions)
					.WithOne(i => i.Card)
					.HasForeignKey(i => i.CardId)
					.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
