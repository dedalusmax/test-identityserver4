using System;
using System.Collections.Generic;

namespace Test.WebAPI.Swagger.RealData.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : UserBase
    {
        public string Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }

    public class UserBase
    {
        public Guid? EnterpriseId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
