using PaymentService.Domain.Entities.Cards;

namespace PaymentService.Application.Cards.Queries.GetByUserId
{
	internal class GetByUserIdQueryHandler(ICardRepository repository, IMapper mapper)
		: IRequestHandler<GetByUserIdQuery, Result<CardDto>>
	{
		public async Task<Result<CardDto>> Handle(GetByUserIdQuery request, CancellationToken cancellationToken)
		=> Result.Create(await repository.GetByUserIdAsync(request.UserId, cancellationToken))
					.MapFailure(CardErrors.UserDoesNotHaveCard)
					.Map(mapper.Map<CardDto>);
	}
}
