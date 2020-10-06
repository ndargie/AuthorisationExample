using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesttIdentityServer
{
    public static class Configuration
    {

        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>() {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()                
            };

        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource>()
            {
                new ApiResource("ApiOne")
                {
                    Scopes = {"ApiOne" },
                    UserClaims = {JwtClaimTypes.Name, 
                        JwtClaimTypes.Id}
                }                
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>() {
                new Client() {
                    ClientId = "client_id_mvc",
                    ClientSecrets = {new Secret("client_secret_mvc".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:44363/signin-oidc"},
                    PostLogoutRedirectUris = { "https://localhost:44363/home/index" },
                    AllowedScopes =
                    {
                        "ApiOne",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        JwtClaimTypes.Id
                    },
                    RequireConsent = false,
                    AllowOfflineAccess = true
                }
                // puts all claims in the id token
                    //AlwaysIncludeUserClaimsInIdToken = true
            };

        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope>
            {
                new ApiScope("ApiOne")                
            };
    }
}
