using Microsoft.AspNetCore.Identity;
using Test.IdentityServer4.Data.Interfaces;

namespace Test.IdentityServer4.Data
{
    public class SeedService
    {
        private readonly IDatabaseScope _database;
        private readonly UserManager<Entities.ApplicationUser> _userManager;
        private readonly RoleManager<Entities.Role> _roleManager;

        public SeedService(IDatabaseScope database, UserManager<Entities.ApplicationUser> userManager, RoleManager<Entities.Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _database = database;
        }

        public void Seed()
        {
            SeedRoles();
            SeedUsers();
            _database.SaveAsync().Wait();
        }

        private void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("SuperAdmin").Result)
            {
                Entities.Role role = new Entities.Role
                {
                    Name = "SuperAdmin",
                    Description = "I am a superadmin role."
                };
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        private void SeedUsers()
        {
            if (_userManager.FindByNameAsync("admin").Result == null)
            {
                Entities.ApplicationUser user = new Entities.ApplicationUser
                {
                    UserName = "admin@test.eu",
                    Email = "admin@test.eu",
                    PasswordHash = "xITMYN3zMD9p3SIoSwmCpoLsC31Nvpb1KvKbfVTorfU=",
                    PasswordSalt = "4qa+cSuEiH87KF2TSOGilw==",
                    FirstName = "test",
                    LastName = "Admin"
                };

                IdentityResult result = _userManager.CreateAsync(user).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "SuperAdmin").Wait();
                }
            }
        }
    }
}
