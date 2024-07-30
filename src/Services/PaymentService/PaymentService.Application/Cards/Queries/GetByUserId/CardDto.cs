using PaymentService.Domain.Entities.Cards;

namespace PaymentService.Application.Cards.Queries.GetByUserId
{
	public sealed class CardDto : IMapWith<Card>
	{
		public string CardNumber { get; set; }
		public string CardHolderName { get; set; }
		public DateOnly CardExpirationDate { get; set; }

		public void Mapping(Profile profile) =>
			profile.CreateMap<Card, CardDto>()
				.ForMember(dest => dest.CardNumber, opt => opt.MapFrom<CardNumberResolver>());
	}

	public class CardNumberResolver : IValueResolver<Card, CardDto, string>
	{
		public string Resolve(Card source, CardDto destination, string destMember, ResolutionContext context)
		{
			return MaskCardNumber(source.CardNumber);
		}

		private string MaskCardNumber(string cardNumber)
		{
			if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 6)
				return cardNumber;

			return $"{cardNumber.Substring(0, 2)}{new string('*', cardNumber.Length - 6)}{cardNumber[^4..]}";
		}
	}
}
