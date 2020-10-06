using IdentityServer4.Models;
using System.Collections.Generic;

namespace Zetes.IdentityServer.Data
{
    public static class ApiResourceDefinitions
    {
        public static IEnumerable<ApiResource> GetApis() =>
          new List<ApiResource>()
          {
                new ApiResource("ApiRestService")
                {
                    Scopes = { "ApiRestService" }
                }             
          };

    }
}
