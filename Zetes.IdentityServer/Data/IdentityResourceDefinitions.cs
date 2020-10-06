using IdentityServer4.Models;
using System.Collections.Generic;

namespace Zetes.IdentityServer.Data
{
    public static class IdentityResourceDefinitions
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>() {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
    }
}
