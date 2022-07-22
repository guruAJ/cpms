using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Helpers
{
    public class TokenRequest
    {

        [FromForm(Name = "grant_type")]
        public string GrantType { get; set; }
        [FromForm(Name = "scope")]
        public string Scope { get; set; }
        [FromForm(Name = "refresh_token")]
        public string RefreshToken { get; set; }
    }
}
