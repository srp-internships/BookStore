namespace PaymentService.Application.Cards
{
	public static class CardErrors
	{
		public static NotFoundError UserDoesNotHaveCard()
			=> new("Card.NotFound", "User does not have any card registered.");
	}
}
