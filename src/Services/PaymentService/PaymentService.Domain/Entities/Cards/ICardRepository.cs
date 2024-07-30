namespace PaymentService.Domain.Entities.Cards
{
	public interface ICardRepository
	{
		Card Create(Card card);
		void Updated(Card card);
		void MarkDeleted(Card card);

		Task<Card?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
	}
}
