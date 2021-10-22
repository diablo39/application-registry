using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationRegistry.Web.Models.Configuration
{
    public class FrontendConfigurationModel
    {
        public OidcAuthenticationConfigurationModel Authentication { get; set; }
    }

    public class OidcAuthenticationConfigurationModel
    {
        public string Authority { get; set; }

        public string ClientId { get; set; }

        public string RedirectUri { get; set; }

        public string ResponseType { get; set; }

        public string Scope { get; set; }

        public string PostLogoutRedirectUri { get; set; }

        public string SilentRedirectUri { get; set; }
    }
}
