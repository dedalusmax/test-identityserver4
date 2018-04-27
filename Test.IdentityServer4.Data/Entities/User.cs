using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.IdentityServer4.Data.Entities
{
    [Table("Users")]
    public class User : IdentityUser
    {
        //[ForeignKey("Enterprise")]
        //public Guid? EnterpriseId { get; set; }

        [StringLength(255)]
        public string PasswordSalt { get; set; }

        //public virtual Enterprise Enterprise { get; set; }

        [StringLength(255)]
        public string PasswordResetCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? HasBeenDeleted { get; set; }
    }
}
