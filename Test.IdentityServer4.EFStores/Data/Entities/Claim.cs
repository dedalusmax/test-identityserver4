using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Test.IdentityServer4.EFStores.Data.Entities

{
    public class Claim : IEntity
    {
        [Key]
        public long Id { get; set; }

        public long UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public string Issuer { get; set; }

        [Required]
        public string OriginalIssuer { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public string ValueType { get; set; }
    }
}
