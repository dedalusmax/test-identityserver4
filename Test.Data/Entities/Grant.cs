using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Test.Data.Entities
{
    public class Grant : IEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        [Required]
        public string Data { get; set; }

        public DateTime? Expiration { get; set; }

        [Required]
        public string SubjectId { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
