using PaymentService.Domain.Entities.Cards;
using PaymentService.Domain;

namespace PaymentService.Application.Cards.Commands.AddOrUpdateCard
{
	internal class AddOrUpdateCardCommandHandler(ICardRepository repository, IUnitOfWork db)
		: IRequestHandler<AddOrUpdateCardCommand, Result>
	{
		public async Task<Result> Handle(AddOrUpdateCardCommand request, CancellationToken cancellationToken)
		{
			var userCard = await repository.GetByUserIdAsync(request.UserId, cancellationToken);

			if (userCard != null)
				repository.MarkDeleted(userCard);

			var newCard = new Card
			{
				Id = Guid.NewGuid(),
				IsDeleted = false,
				UserId = request.UserId,
				CardHolderRole = request.CardHolderRole,
				CardNumber = request.CardNumber,
				CardCvc = request.CardCvc,
				CardHolderName = request.CardHolderName,
				CardExpirationDate = request.CardExpirationDate,
			};

			repository.Create(newCard);
			await db.SaveChangesAsync(cancellationToken);

			return Result.Success();
		}
	}
}
