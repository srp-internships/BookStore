using Duende.IdentityServer.Services;
using IdentityService.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace Microsoft.AspNetCore.Routing
{
	internal static class IdentityComponentsEndpointRouteBuilderExtensions
	{
		// These endpoints are required by the Identity Razor components defined in the /Components/Account/Pages directory of this project.
		public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
		{
			ArgumentNullException.ThrowIfNull(endpoints);

			var accountGroup = endpoints.MapGroup("/Account");

			accountGroup.MapPost("/Logout", async (
				ClaimsPrincipal user,
				SignInManager<User> signInManager,
				IIdentityServerInteractionService interactionService,
				[FromQuery] string? logoutId) =>
			{
				await signInManager.SignOutAsync();
				var logoutResult = await interactionService.GetLogoutContextAsync(logoutId);

				if (string.IsNullOrEmpty(logoutResult.PostLogoutRedirectUri))
				{
					return TypedResults.LocalRedirect($"~/");
				}

				return Results.Redirect(logoutResult.PostLogoutRedirectUri);
			});

			var manageGroup = accountGroup.MapGroup("/Manage").RequireAuthorization();

			var loggerFactory = endpoints.ServiceProvider.GetRequiredService<ILoggerFactory>();
			var downloadLogger = loggerFactory.CreateLogger("DownloadPersonalData");

			manageGroup.MapPost("/DownloadPersonalData", async (
				HttpContext context,
				[FromServices] UserManager<User> userManager,
				[FromServices] AuthenticationStateProvider authenticationStateProvider) =>
			{
				var user = await userManager.GetUserAsync(context.User);
				if (user is null)
				{
					return Results.NotFound($"Unable to load user with ID '{userManager.GetUserId(context.User)}'.");
				}

				var userId = await userManager.GetUserIdAsync(user);
				downloadLogger.LogInformation("User with ID '{UserId}' asked for their personal data.", userId);

				// Only include personal data for download
				var personalData = new Dictionary<string, string>();
				var personalDataProps = typeof(User).GetProperties().Where(
					prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
				foreach (var p in personalDataProps)
				{
					personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
				}

				personalData.Add("Authenticator Key", (await userManager.GetAuthenticatorKeyAsync(user))!);
				var fileBytes = JsonSerializer.SerializeToUtf8Bytes(personalData);

				context.Response.Headers.TryAdd("Content-Disposition", "attachment; filename=PersonalData.json");
				return TypedResults.File(fileBytes, contentType: "application/json", fileDownloadName: "PersonalData.json");
			});

			return accountGroup;
		}
	}
}
