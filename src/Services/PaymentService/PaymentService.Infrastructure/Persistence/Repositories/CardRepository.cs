using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Entities.Cards;

namespace PaymentService.Infrastructure.Persistence.Repositories
{
	internal class CardRepository(AppDbContext context) : ICardRepository
	{
		private readonly DbSet<Card> cards = context.Set<Card>();

		public Card Create(Card card)
		{
			cards.Add(card);
			return card;
		}

		public Task<Card?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
		{
			return cards.FirstOrDefaultAsync(i => i.UserId == userId, cancellationToken);
		}

		public void MarkDeleted(Card card)
		{
			card.IsDeleted = true;
			Updated(card);
		}

		public void Updated(Card card)
		{
			cards.Update(card);
		}
	}
}
