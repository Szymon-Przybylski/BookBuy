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

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // just for development
                new Client
                {
                    ClientId = "postman",
                    ClientName = "postman",
                    AllowedScopes = {"openid", "profile", "auctionApplication" },
                    RedirectUris = {"https://www.getpostman.com/funstuff/oauth2/callback"},
                    ClientSecrets = new[] {new Secret("NotASecret".Sha256())},
                    AllowedGrantTypes = {GrantType.ResourceOwnerPassword},
                }
            };
    }
}
