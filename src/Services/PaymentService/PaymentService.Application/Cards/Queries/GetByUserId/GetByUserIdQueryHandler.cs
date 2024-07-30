using PaymentService.Application.Common.Helpers;
using PaymentService.Domain.Entities.Cards;
using PaymentService.Domain.Shared.Helpers;

namespace PaymentService.Application.Cards.Queries.GetByUserId
{
	internal class GetByUserIdQueryHandler(ICardRepository repository, IMapper mapper)
		: IRequestHandler<GetByUserIdQuery, Result<CardDto>>
	{
		public async Task<Result<CardDto>> Handle(GetByUserIdQuery request, CancellationToken cancellationToken)
		=> Result.Create(await repository.GetByUserIdAsync(request.UserId, cancellationToken))
					.MapFailure(CardErrors.UserDoesNotHaveCard)
					.Tap(i =>
					{
						i.CardHolderName = AesCbcEncryptor.Decrypt(i.CardHolderName, Constants.AesCbcKey);
						i.CardNumber = AesCbcEncryptor.Decrypt(i.CardNumber, Constants.AesCbcKey);
					})
					.Map(mapper.Map<CardDto>);
	}
}
