using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationRegistry.Web.Models
{
    public class ApplicationConfiguration
    {
        public AuthenticationConfiguration Authentication { get; set; }
    }

    public class AuthenticationConfiguration
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
