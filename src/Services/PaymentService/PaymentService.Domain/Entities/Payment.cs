namespace PaymentService.Domain.Entities
{
	public class Payment
	{
		public Guid Id { get; set; }

		public Guid OrderId { get; set; }
		public DateTime RequestedAtUtc { get; set; }
		public DateTime ProcessedAtUtc { get; set; }
		public Transaction CustomerTransaction { get; set; }
		public List<Transaction> SellersTransaction { get; set; }
	}
}