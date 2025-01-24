using Duende.IdentityServer.Models;

namespace IdentityService
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("auctionApplication", "Auction application - full access"),
            };

        public static IEnumerable<Client> Clients(IConfiguration configuration) =>
            new Client[]
            {
                // just for development - postman client
                new Client
                {
                    ClientId = "postman",
                    ClientName = "postman",
                    AllowedScopes = {"openid", "profile", "auctionApplication" },
                    RedirectUris = {"https://www.getpostman.com/funstuff/oauth2/callback"},
                    ClientSecrets = new[] {new Secret("NotASecret".Sha256())},
                    AllowedGrantTypes = {GrantType.ResourceOwnerPassword},
                },
                new Client
                {
                    ClientId = "clientApplication",
                    ClientName = "clientApplication",
                    AllowedScopes = {"openid", "profile", "auctionApplication" },
                    RedirectUris = {configuration["ClientApp"] + "/api/auth/callback/id-server"},
                    ClientSecrets = {new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequirePkce = false,
                    AllowOfflineAccess = true,
                    // just for development - monthly token
                    AccessTokenLifetime = 3600*24*30,
                    AlwaysIncludeUserClaimsInIdToken = true,
                }
            };
    }
}
