using PaymentService.Domain.Entities.Cards;
using PaymentService.Domain;
using PaymentService.Domain.Shared.Helpers;
using PaymentService.Application.Common.Helpers;

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
				CardNumber = AesCbcEncryptor.Encrypt(request.CardNumber, Constants.AesCbcKey),
				CardCvc = AesCbcEncryptor.Encrypt(request.CardCvc, Constants.AesCbcKey),
				CardHolderName = AesCbcEncryptor.Encrypt(request.CardHolderName, Constants.AesCbcKey),
				CardExpirationDate = request.CardExpirationDate,
			};

			repository.Create(newCard);
			await db.SaveChangesAsync(cancellationToken);

			return Result.Success();
		}
	}
}
