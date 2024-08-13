using IdentityService.Data;
using IdentityService.Services;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Components.Account
{
	internal sealed class IdentityEmailSender(IMailService smtpClient) : IEmailSender<User>
	{
		public Task SendConfirmationLinkAsync(User user, string email, string confirmationLink) =>
			smtpClient.SendEmailAsync(email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

		public Task SendPasswordResetLinkAsync(User user, string email, string resetLink) =>
			smtpClient.SendEmailAsync(email, "Reset your password", $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");

		public Task SendPasswordResetCodeAsync(User user, string email, string resetCode) =>
			smtpClient.SendEmailAsync(email, "Reset your password", $"Please reset your password using the following code: {resetCode}");
	}
}
