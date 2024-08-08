using Duende.IdentityServer.Models;
using Duende.IdentityServer;

namespace IdentityService.Components.IDS
{
	public static class IdsConfig
	{
		public static IEnumerable<IdentityResource> IdentityResources =>
			[
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
				new IdentityResources.Email(),
				new IdentityResources.Address(),
				new IdentityResources.Phone(),
			];

		public static IEnumerable<ApiScope> ApiScopes =>
			[
				new ApiScope("common_scope", "Common scope", [ "role", "name" ]),
			];

		public static IEnumerable<ApiResource> ApiResources =>
			[
				new ApiResource("payment", "Payment service")
				{
					Scopes = { "common_scope" }
				},
			];

		public static IEnumerable<Client> Clients =>
			[
				new Client
				{
					ClientId = "swagger-client-3F9610DD-0032-41FA-92F5-397E6B66AE15",
					ClientName = "Swagger UI",

					RequireClientSecret = true,
					ClientSecrets = { new Secret("swagger-ui-DF669678-66B8-4982-890A-E52F7632A3BA".Sha256()) },

					Enabled = true,

					AllowedGrantTypes = GrantTypes.Code,

					RedirectUris = { "https://localhost:7096/signin-oidc" },
					PostLogoutRedirectUris = { "https://localhost:7096/signout-callback-oidc" },

					AllowedScopes =
					[
						"common_scope",
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
					],

					AllowAccessTokensViaBrowser = true,

					AlwaysIncludeUserClaimsInIdToken = true,

					AccessTokenLifetime = 60 * 60,

					AllowedCorsOrigins =
					{
						"https://localhost:7096",
					},

					RequireConsent = false,

					RequirePkce = true,
				}
			];
	}

}
