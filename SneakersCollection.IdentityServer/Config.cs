using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;
using System.Security.Claims;
using System.Text.Json;

namespace SneakersCollection.Api
{
    public class Config
    {
        public static List<TestUser> Users
        {
            get
            {
                var address = new
                {
                    street_address = "One Hacker Way",
                    locality = "Heidelberg",
                    postal_code = 69118,
                    country = "Germany"
                };

                return new List<TestUser>
                {
                  new TestUser
                  {
                    SubjectId = "818727",
                    Username = "alice",
                    Password = "alice",
                    Claims =
                    {
                      new Claim(JwtClaimTypes.Name, "Alice Smith"),
                      new Claim(JwtClaimTypes.GivenName, "Alice"),
                      new Claim(JwtClaimTypes.FamilyName, "Smith"),
                      new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                      new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                      new Claim(JwtClaimTypes.Role, "admin"),
                      new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                      new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address),
                        IdentityServerConstants.ClaimValueTypes.Json)
                    }
                  },
                  new TestUser
                  {
                    SubjectId = "88421113",
                    Username = "bob",
                    Password = "bob",
                    Claims =
                    {
                      new Claim(JwtClaimTypes.Name, "Bob Smith"),
                      new Claim(JwtClaimTypes.GivenName, "Bob"),
                      new Claim(JwtClaimTypes.FamilyName, "Smith"),
                      new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                      new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                      new Claim(JwtClaimTypes.Role, "user"),
                      new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                      new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address),
                        IdentityServerConstants.ClaimValueTypes.Json)
                    }
                  }
                };
            }
        }

        public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                  Name = "role",
                  UserClaims = new List<string> {"role"}
                }
            };

            public static IEnumerable<ApiScope> ApiScopes =>
                new[]
                {
                new ApiScope("sneakerapi.read"),
                new ApiScope("sneakerapi.write"),
                };
            public static IEnumerable<ApiResource> ApiResources => new[]
            {
                new ApiResource("sneakerapi")
                {
                Scopes = new List<string> { "sneakerapi.read", "sneakerapi.write"},
                ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                UserClaims = new List<string> {"role"}
                }
            };

            public static IEnumerable<Client> Clients =>
                new[]
                {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("82124972-4616-4d29-9c16-ccd29bc5d157".Sha256())},

                    AllowedScopes = { "sneakerapi.read", "sneakerapi.write" },
                    RedirectUris = { "https://localhost:44347/swagger/oauth2-redirect.html" },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:44347"
                    }
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = {new Secret("2a331e05-0190-48b0-a08b-9704eb810e66".Sha256())},

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = {"https://localhost:7145/signin-oidc"},
                    FrontChannelLogoutUri = "https://localhost:7145/signout-oidc",
                    PostLogoutRedirectUris = {"https://localhost:7145/signout-callback-oidc"},

                    AllowOfflineAccess = true,
                    AllowedScopes = {"openid", "profile", "sneakerapi.read"},
                    RequirePkce = true,
                    RequireConsent = true,
                    AllowPlainTextPkce = false
                },
            };
    }
}
