using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.IdentityServer4.Data.Entities
{
    [Table("Users")]
    public class ApplicationUser : IdentityUser
    {
        [StringLength(255)]
        public string PasswordSalt { get; set; }

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
