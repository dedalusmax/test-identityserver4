using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.IdentityServer4.Data.Entities
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
    }
}
