using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Test.IdentityServer4.RealData.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

    }

//    public class ApplicationUser : UserBase
//    {
//        public string Id { get; set; }

//        public DateTime Created { get; set; }

//        public DateTime Updated { get; set; }

//        public bool EmailConfirmed { get; set; }

//        public bool TwoFactorEnabled { get; set; }
//}

//    public class UserBase
//    {
//        public Guid? EnterpriseId { get; set; }

//        public string UserName { get; set; }

//        public string FirstName { get; set; }

//        public string LastName { get; set; }

//        public string Email { get; set; }

//        public string PhoneNumber { get; set; }

//        public string Password { get; set; }

//        public IEnumerable<string> Roles { get; set; } = new List<string>();
//    }
}