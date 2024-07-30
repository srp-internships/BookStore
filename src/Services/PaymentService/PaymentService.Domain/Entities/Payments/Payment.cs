namespace PaymentService.Domain.Entities.Payments
{
	public class Payment
	{
		public Guid Id { get; set; }

		public Guid OrderId { get; set; }
		public DateTime RequestedAtUtc { get; set; }
		public DateTime? ProcessedAtUtc { get; set; }
		public List<Transaction> Transaction { get; set; }
	}
}