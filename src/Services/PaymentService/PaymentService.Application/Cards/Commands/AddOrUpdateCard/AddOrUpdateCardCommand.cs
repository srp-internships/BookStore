using PaymentService.Domain.Entities.Cards;

namespace PaymentService.Application.Cards.Commands.AddOrUpdateCard
{
	public class AddOrUpdateCardCommand : IRequest<Result>
	{
		public Guid UserId { get; set; }
		public CardHolderRole CardHolderRole { get; set; }
		public string CardNumber { get; set; }
		public string CardCvc { get; set; }
		public string CardHolderName { get; set; }
		public DateOnly CardExpirationDate { get; set; }
	}
}
