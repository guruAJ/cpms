using CPMS_AUTO.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Helpers
{
    public class Account
    {
        public IConfiguration Configuration { get; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public Account(IConfiguration configuration, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {

            Configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task getAsync(Login model)
        {
            LdapDirectoryIdentifier ldapDir = new LdapDirectoryIdentifier(Configuration.GetSection("Appsetings").GetSection("ipaddress").Value, 389);
            LdapConnection ldapConn = new LdapConnection(ldapDir);
            ldapConn.AuthType = AuthType.Basic;
            // System.Net.NetworkCredential myCredentials = new System.Net.NetworkCredential("uid=" + model.Username + ",ou=people, o = army.mil, dc=army,dc =mil", model.Password);
            System.Net.NetworkCredential myCredentials = new System.Net.NetworkCredential("CN=" + model.UserID + ", CN = Users, dc=LDAP,dc =COM", model.Password);
            ldapConn.Bind(myCredentials);

            var result = await _signInManager.PasswordSignInAsync(model.UserID, model.Password, model.RememberLogin, lockoutOnFailure: true);
        }
    }
    
}
