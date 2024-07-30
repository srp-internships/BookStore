namespace PaymentService.Application.Common.Inbox
{
	public sealed class InboxMessage
	{
		public InboxMessage(Guid id, DateTime occurredOnUtc, string type, string content)
		{
			Id = id;
			OccurredOnUtc = occurredOnUtc;
			Content = content;
			Type = type;
		}

		public Guid Id { get; private set; }

		public DateTime OccurredOnUtc { get; private set; }

		public string Type { get; private set; }

		public string Content { get; private set; }

		public DateTime? ProcessedOnUtc { get; set; }

		public string? Error { get; set; }
	}
}
