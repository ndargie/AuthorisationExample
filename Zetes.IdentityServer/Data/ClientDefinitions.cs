using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Zetes.IdentityServer.Models.Configuration;

namespace Zetes.IdentityServer.Data
{
    public static class ClientDefinitions
    {

        public static IEnumerable<Client> GetClients(IConfiguration config)
        {
            var clientConfig = config.GetSection("Client").Get<ClientConfig>();
            return new List<Client>() {
                   new Client() {
                       ClientId = clientConfig.ClientId,
                       ClientSecrets = {new Secret(((string)config.GetValue(typeof(string), clientConfig.Secret)).Sha256()) },
                       AllowedGrantTypes = GrantTypes.Code,
                       RedirectUris = { clientConfig.RedirectUri},
                       PostLogoutRedirectUris = { clientConfig.PostLogoutRedirectUri },
                       AllowedScopes =
                       {
                           "ApiRestService",
                           IdentityServerConstants.StandardScopes.OpenId,
                           IdentityServerConstants.StandardScopes.Profile,
                       },
                       RequireConsent = false,
                       AllowOfflineAccess = true
                   }
            };
        }
    }
}
