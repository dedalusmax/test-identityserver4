using System;
using System.ComponentModel.DataAnnotations;

namespace Test.IdentityServer4.EFStores.Data.Entities
{
    public class Secret
    {
        public string Description { get; set; }

        [Key]
        public string Value { get; set; }

        public DateTime? Expiration { get; set; }

        public string Type { get; set; }
    }
}
