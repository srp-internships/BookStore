using Duende.IdentityServer;
using Duende.IdentityServer.Models;

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
                new ApiResource("book_program", "Book program")
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

                    RedirectUris = {
                        "https://localhost:7096/swagger/oauth2-redirect.html",
                        "https://localhost:7257/swagger/oauth2-redirect.html",
                        "https://localhost:7209/swagger/oauth2-redirect.html",
                        "https://localhost:7132/swagger/oauth2-redirect.html",
                        "https://localhost:7309/swagger/oauth2-redirect.html",
                        "https://localhost:3040/swagger/oauth2-redirect.html",

                    },
                    PostLogoutRedirectUris = {
                        "https://localhost:7096/swagger/signout-callback-oidc",
                        "https://localhost:7257/swagger/signout-callback-oidc",
                        "https://localhost:7209/swagger/signout-callback-oidc",
                        "https://localhost:7132/swagger/signout-callback-oidc",
                        "https://localhost:7309/swagger/signout-callback-oidc",
                        "https://localhost:3040/swagger/signout-callback-oidc",
                    },

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
                         "https://localhost:7257",
                          "https://localhost:7209",
                           "https://localhost:7132",
                            "https://localhost:7309",
                             "https://localhost:3040"
                    },

                    RequireConsent = false,

                    RequirePkce = true,
                }
            ];
    }

}
