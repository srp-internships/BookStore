namespace PaymentService.Domain.Entities
{
	public class Transaction
	{
		public Guid Id { get; set; }

		public Guid CardId { get; set; }
		public Card Card { get; set; }
		public decimal Amount { get; set; }
		public TransactionType Type { get; set; }
	}
}