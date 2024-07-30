using PaymentService.Domain.Entities.Cards;

namespace PaymentService.Application.Cards.Queries.GetByUserId
{
	public sealed class CardDto : IMapWith<Card>
	{
		public string CardNumber { get; set; }
		public string CardHolderName { get; set; }
		public DateTime CardExpirationDate { get; set; }

		public void Mapping(Profile profile) =>
			profile.CreateMap<Card, CardDto>()
				.ForMember(dest => dest.CardNumber, opt => opt.MapFrom(o => $"{o.CardNumber.Substring(0, 2)}{new string('*', o.CardNumber.Length - 6)}{o.CardNumber.Substring(o.CardNumber.Length - 4)}"))
				.ForMember(dest => dest.CardExpirationDate, opt => opt.MapFrom(o => o.CardExpirationDate.ToDateTime(TimeOnly.FromTimeSpan(TimeSpan.Zero))));
	}
}
