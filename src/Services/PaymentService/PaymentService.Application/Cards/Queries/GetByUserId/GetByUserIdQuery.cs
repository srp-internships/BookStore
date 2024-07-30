namespace PaymentService.Application.Cards.Queries.GetByUserId
{
	public class GetByUserIdQuery : IRequest<Result<CardDto>>
	{
		public Guid UserId { get; set; }
	}
}
