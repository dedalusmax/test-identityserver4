using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Data.Entities
{
    public class User : IEntity
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Role")]
        public long? RoleId { get; set; }

        [Required, MaxLength(255)]
        public string UserName { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string MobilePhone { get; set; }

        [MaxLength(255)]
        public string AlternativePhone { get; set; }

        [MaxLength(255)]
        public string DisplayName { get; set; }

        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [MaxLength(255)]
        public string PasswordSalt { get; set; }

        public virtual Role Role { get; set; }

        [Required]
        public Language Language { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }

    public enum Language
    {
        English = 1,
        German = 2,
        French = 3
    }
}
