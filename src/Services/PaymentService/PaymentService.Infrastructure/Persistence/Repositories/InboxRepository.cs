using Microsoft.EntityFrameworkCore;
using PaymentService.Application.Common.Inbox;

namespace PaymentService.Infrastructure.Persistence.Repositories
{
	internal class InboxRepository(AppDbContext context) : IInboxRepository
	{
		private readonly DbSet<InboxMessage> messages = context.Set<InboxMessage>();

		public InboxMessage Create(InboxMessage message)
		{
			messages.Add(message);
			return message;
		}
	}
}
