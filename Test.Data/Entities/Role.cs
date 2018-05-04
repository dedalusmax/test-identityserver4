using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Data.Entities
{
    public class Role : IEntity
    {
        public const string Administrator = "Administrator";
        public const string Guest = "Guest";

        [Key]
        public long Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        internal string _Permissions { get; set; }

        [NotMapped]
        public Permissions Permissions
        {
            get { return JsonConvert.DeserializeObject<Permissions>(_Permissions); }
            set { _Permissions = JsonConvert.SerializeObject(value); }
        }
    }
}
