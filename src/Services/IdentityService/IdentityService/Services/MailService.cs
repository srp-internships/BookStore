using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace IdentityService.Services
{
	public class MailService(SmtpClient smtpClient, IOptions<SmtpSettings> options) : IMailService
	{
		public async Task SendEmailAsync(string email, string subject, string htmlMessage, CancellationToken cancellationToken = default)
		{
			MailMessage mailMessage = new MailMessage();
			mailMessage.From = new MailAddress(options.Value.From);
			mailMessage.To.Add(email);
			mailMessage.Subject = subject;
			mailMessage.Body = htmlMessage;
			mailMessage.IsBodyHtml = true;

			await smtpClient.SendMailAsync(mailMessage, cancellationToken);
		}
	}
}
