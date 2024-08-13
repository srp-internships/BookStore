namespace IdentityService.Services
{
	public interface IMailService
	{
		Task SendEmailAsync(string email, string subject, string htmlMessage, CancellationToken cancellationToken = default);
	}
}
