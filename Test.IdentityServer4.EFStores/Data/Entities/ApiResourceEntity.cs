using IdentityServer4.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.IdentityServer4.EFStores.Data.Entities
{
    public class ApiResourceEntity
    {
        //[Key]
        public long Id { get; set; }

        public string Description { get; set; }

        public string DisplayName { get; set; }

        public bool Enabled { get; set; }
        
        [Required] // unique
        public string Name { get; set; }

        /*Don't know how to map these two since in the original ApiResource model they use real Secret and Claim objects*/
        public ICollection<string> ApiSecrets { get; set; }

        public ICollection<string> UserClaims { get; set; }

        [Required]
        public ICollection<string> Scopes { get; set; }

        /*For the reference: https://identityserver4.readthedocs.io/en/release/reference/api_resource.html*/

        public string ApiResourceData { get; set; }

        [NotMapped]
        public ApiResource ApiResource { get; set; }

        public void AddDataToEntity()
        {
            ApiResourceData = JsonConvert.SerializeObject(ApiResource);
            Name = ApiResource.Name;
        }

        public void MapDataFromEntity()
        {
            ApiResource = JsonConvert.DeserializeObject<ApiResource>(ApiResourceData);
            Name = ApiResource.Name;
        }
    }
}
