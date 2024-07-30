namespace PaymentService.Application.Common.Inbox
{
	public interface IInboxRepository
	{
		InboxMessage Create(InboxMessage message);
	}
}
