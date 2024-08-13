using PaymentService.Domain.Entities.Cards;

namespace PaymentService.Domain.Entities.Payments
{
	public class Transaction
	{
		public Guid Id { get; set; }

		public decimal Amount { get; set; }
		public TransactionType Type { get; set; }

		public Guid CardId { get; set; }
		public Card Card { get; set; }

		public Guid PaymentId { get; set; }
		public Payment Payment { get; set; }
	}
}