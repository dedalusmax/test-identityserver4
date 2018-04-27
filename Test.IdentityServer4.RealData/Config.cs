using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Test.IdentityServer4.RealData
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            /*
            var customProfile = new IdentityResource(
                name: customProfileName,
                displayName: "Custom profile",
                claimTypes: new[] { "name", "email", "given_name" }
            );
            */
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email { Required = true }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            var api = new ApiResource("api:admin", "Admin API");

            // api.UserClaims.Add(IdentityModel.JwtClaimTypes.Email);
            // api.UserClaims.Add(IdentityModel.JwtClaimTypes.GivenName);
            // api.UserClaims.Add(IdentityModel.JwtClaimTypes.FamilyName);
            // api.UserClaims.Add(IdentityModel.JwtClaimTypes.Name);
            return new List<ApiResource>
            {
                api
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "js_demo",
                    ClientName = "Demo JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { "http://localhost:5003/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5003" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api:admin"
                    }
                },

                new Client
                {
                    ClientId = "adminswaggerui",
                    ClientName = "Admin Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { "http://localhost:50641/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { "http://localhost:50641/swagger/" },

                    AllowedScopes =
                    {
                        "api:admin"
                    }
                },

                new Client
                {
                    ClientId = "js_angular_admin",
                    ClientName = "Angular Admin JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { "http://localhost:4200/login" },
                    PostLogoutRedirectUris = { "http://localhost:4200" },
                    AllowedCorsOrigins = { "http://localhost:4200" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api:admin"
                    }
                }
            };
        }
    }
}
