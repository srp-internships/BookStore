using PaymentService.Domain.Entities.Payments;

namespace PaymentService.Domain.Entities.Cards
{
	public class Card
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public bool IsDeleted { get; set; } = false;

		public Guid UserId { get; set; }
		public CardHolderRole CardHolderRole { get; set; }
		public string CardNumber { get; set; }
		public string CardCvc { get; set; }
		public string CardHolderName { get; set; }
		public DateOnly CardExpirationDate { get; set; }

		public List<Transaction> Transactions { get; set; }
	}
}
