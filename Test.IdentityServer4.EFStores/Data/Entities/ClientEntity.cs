using IdentityServer4.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.IdentityServer4.EFStores.Data.Entities
{
    public class ClientEntity
    {
        //[Key] // unique
        public string ClientId { get; set; }

        public bool Enabled { get; set; }

        public bool RequireClientSecret { get; set; }

        public ICollection<Secret> ClientSecrets { get; set; }// = new HashSet<Secret>();

        // used for logging and consent screen
        public string ClientName { get; set; }  

        // used for consent screen
        public string ClientUri { get; set; }   

        public bool RequireConsent { get; set; }

        public bool AllowRememberConsent { get; set; }

        public ICollection<Grant> AllowedGrantTypes { get; set; }

        // whether a proof key is required for authorization code based token requests
        public bool RequirePkce { get; set; }   

        // whether a proof key can be sent using plain method
        public bool AllowPlainTextPkce { get; set; }    

        // can prevent accidental leakage of access tokens when multiple response types are allowed
        public bool AllowAccessTokensViaBrowser { get; set; }

        // allowed URIs to return tokens or authorization codes to
        public ICollection<string> RedirectUris { get; set; } = new HashSet<string>();

        // allowed URIs to redirect to after logout
        public ICollection<string> PostLogoutRedirectUris { get; set; } = new HashSet<string>();

        public bool AllowOfflineAccess { get; set; }

        // Api scopes that the client is allowed to request
        public string AllowedScopes { get; set; } //= new HashSet<string>();

        // When requesting both an id token and access token, should the user claims always be added
        // to the id token instead of requring the client to use the userinfo endpoint
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }

        // In IS4 Client, this one is called only Claims
        // Allows settings claims for the client (will be included in the access token)
        public ICollection<Claim> ClientClaims { get; set; } = new HashSet<Claim>();

        // whether client claims should be always included in the access tokens - or only for client credentials flow
        public bool AlwaysSendClientClaims { get; set; }

        public ICollection<string> AllowedCorsOrigins { get; set; } = new HashSet<string>();

        /*For the reference: 
         https://identityserver4.readthedocs.io/en/release/reference/client.html 
         https://github.com/IdentityServer/IdentityServer4/blob/dev/src/IdentityServer4/Models/Client.cs 
        */

        public string ClientData { get; set; }

        [NotMapped]
        public Client Client { get; set; }

        public void AddDataToEntity()
        {
            ClientData = JsonConvert.SerializeObject(Client);
            ClientId = Client.ClientId;
        }

        public void MapDataFromEntity()
        {
            Client = JsonConvert.DeserializeObject<Client>(ClientData);
            ClientId = Client.ClientId;
        }
    }
}
