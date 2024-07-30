using PaymentService.Application.Common.Helpers;

namespace PaymentService.Application.Cards.Commands.AddOrUpdateCard
{
	public class AddOrUpdateCardCommandValidator : AbstractValidator<AddOrUpdateCardCommand>
	{
		public AddOrUpdateCardCommandValidator()
		{
			RuleFor(i => i.UserId).NotEmpty();

			RuleFor(i => i.CardHolderRole).IsInEnum();

			RuleFor(i => i.CardNumber)
				.NotEmpty()
					.WithMessage("Card number is required.")
				.Matches(Constants.CardNumberRegExPattern)
					.WithMessage("Invalid Visa card number.");

			RuleFor(i => i.CardCvc)
				.NotEmpty()
					.WithMessage("CVC is required.")
				.Matches(Constants.CardCvcRegExPattern)
					.WithMessage("Invalid CVC. It should be exactly 3 digits.");

			RuleFor(i => i.CardHolderName).NotEmpty();

			RuleFor(i => i.CardExpirationDate)
				.NotEmpty()
				.Must(i => i > DateOnly.FromDateTime(DateTime.UtcNow))
					.WithMessage("The card has expired or will expire soon.");
		}
	}
}
