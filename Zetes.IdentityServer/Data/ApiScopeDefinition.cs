using IdentityServer4.Models;
using System.Collections.Generic;

namespace Zetes.IdentityServer.Data
{
    public static class ApiScopeDefinition
    {
        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope>
            {
                new ApiScope("ApiRestService")
            };
    }
}
