namespace PaymentService.WebApi.Contracts
{
	public class CreateCardDto
	{
		public Guid UserId { get; set; } // TODO delete this when identity server is done
		public string CardNumber { get; set; }
		public string CardCvc { get; set; }
		public string CardHolderName { get; set; }
		public DateTime CardExpirationDate { get; set; }
	}
}
