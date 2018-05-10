using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace Test.IdentityServer4.EFConfigStore
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResources.Address(),
                new IdentityResource(
                name: "custom.identity",
                displayName: "Custom profile",
                claimTypes: new[] { "name", "email", "given_name" }
            )
        };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api:system", "SENG.System.WebAPI")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api:system" }
                },

                new Client
                {
                    ClientId = "js_clientcredentials",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { "http://localhost:5007/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5007/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5007" },

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api:system"
                    }
                },

                new Client
                {
                    ClientId = "adminswaggerui",
                    ClientName = "Admin Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { "http://localhost:5001/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { "http://localhost:5001/swagger/" },

                    AllowedScopes =
                    {
                        "api:system"
                    }
                },

                new Client
                {
                    ClientId = "js_angular_admin",
                    ClientName = "Angular Admin JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { "http://localhost:4202/login" },
                    PostLogoutRedirectUris = { "http://localhost:4202" },
                    AllowedCorsOrigins = { "http://localhost:4202" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api:system"
                    }
                },

                new Client
                {
                    ClientId = "js_demo",
                    ClientName = "Demo JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    //RequireConsent = false,

                    RedirectUris = { "http://localhost:5003/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5003" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api:system"
                    }
                },

                new Client
                {
                    ClientId = "mvc",
                    ClientName = "McGuireV10.com",
                    ClientUri = "http://localhost:5002",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowRememberConsent = true,
                    AllowOfflineAccess = true,
                    RedirectUris = { "http://localhost:5002/signin-oidc"}, // after login
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc"}, // after logout
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Address,
                        "api:system"
                    }
                }
            };
        }
    }
}
