using IdentityServer4.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.IdentityServer4.EFStores.Data.Entities
{
    public class IdentityResourceEntity
    {
        //[Key]
        public long Id { get; set; }

        [Required] // unique
        public string Name { get; set; }

        public string Description { get; set; }

        public string DisplayName { get; set; }

        public bool Emphasize { get; set; }

        public bool Enabled { get; set; }

        public bool Required { get; set; }

        public bool ShowInDiscoveryDocument { get; set; }

        //[Required]
        public List<string> UserClaims { get; set; }

        public string IdentityResourceData { get; set; }

        [NotMapped]
        public IdentityResource IdentityResource { get; set; }

        public void AddDataToEntity()
        {
            IdentityResourceData = JsonConvert.SerializeObject(IdentityResource);
            Name = IdentityResource.Name;
        }

        public void MapDataFromEntity()
        {
            IdentityResource = JsonConvert.DeserializeObject<IdentityResource>(IdentityResourceData);
            Name = IdentityResource.Name;
        }
    }
}
