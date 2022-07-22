using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO
{
    public class InhClient
    {
        [Key]
        public string ClientId { get; set; }
        public string ClientName { get; set; }

        public string AllowedGrantTypes { get; set; }

        public string RedirectUris { get; set; }

        public string PostLogoutRedirectUris { get; set; }

        public string AllowOfflineAccess { get; set; }

        public Boolean AllowedScopes { get; set; } = true;

        public string ClientSecrets { get; set; } = Guid.NewGuid().ToString();

        public Boolean AlwaysSendClientClaims { get; set; } = true;

        public Boolean AlwaysIncludeUserClaimsInIdToken { get; set; } = true;
        public string Scope { get; set; }


    }
}
