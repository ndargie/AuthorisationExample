using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zetes.IdentityServer.Models.Configuration
{
    public class ClientConfig
    {
        public string Secret { get; set; } 
        public string ClientId { get; set; }
        public string RedirectUri { get; set; }
        public string PostLogoutRedirectUri { get; set; }
    }
}
