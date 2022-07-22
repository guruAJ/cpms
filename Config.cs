using CPMS_AUTO.Data;
using CPMS_AUTO.Models;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("api1", "My API #1"),
                new ApiResource("all", "all")
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
               

                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "api1" }
                },

                 

                // MVC client using code flow + pkce
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequirePkce = true,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    RedirectUris = { "http://localhost:5003/signin-oidc" },
                    FrontChannelLogoutUri = "http://localhost:5003/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:5003/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "api1" }
                },

                // SPA client using code flow + pkce
          new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA (Code + PKCE)",

                    RequireClientSecret = false,
                    RequireConsent = false,
                    ClientSecrets = { new Secret("61611C1B-B350-8982-897C-3F9214A4875A".Sha256()) },
                    RedirectUris = { "https://localhost:4100" ,"https://localhost:4100/home"},
                    PostLogoutRedirectUris = { "https://localhost:4100","https://localhost:4100/home" },

                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowedScopes = { "openid", "profile"},

                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse
                }
                ,


                new Client
                {
                    ClientId = "MI10",
                    ClientName = "MI_10",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = { "https://localhost:59474/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:59474/" },
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile" , "api1"},
                    ClientSecrets = { new Secret("81315a08-fe25-4441-820f-22cdf1f5e197".Sha256()) },
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                     //54c6a8b6-45ec-4b38-a714-9f8af897aea0
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "angular_spa",
                    ClientName = "Angular SPA",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { "openid", "profile","api1"},
                    RedirectUris = {"http://localhost:4200/auth-callback"},
                    PostLogoutRedirectUris = {"http://localhost:4200/"},
                    AllowedCorsOrigins = {"http://localhost:4200"},
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 3600
                }
            };


        public static IEnumerable<Client> GetClients(DbContextOptions<ApplicationDbContext> _contextOptions)
        {


            var context = new ApplicationDbContext(_contextOptions);

            var data = context.Clients.ToListAsync();
            List<ClientEntity> clients = new List<ClientEntity>();
            // clients.Add(data);// = (ClientEntity)data;
            return null;
        }

    }
}
