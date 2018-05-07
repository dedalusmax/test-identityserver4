using System.Collections.Generic;
using System.Linq;
using Test.Data;
using Test.Data.Entities;
using Test.Library.Services;

namespace Test.Library
{
    public static class DbInitializer
    {
        private static AppDbContext _context;
        
        public static void Seed(AppDbContext context)
        {
            _context = context;

            SeedRoles(context);
            SeedUsers(context);

            _context.SaveChanges();
        }

        public static void SeedRoles(AppDbContext _context)
        {
            var requiredRoles = new List<KeyValuePair<string, Permissions>>();

            var maxPermissions = RoleService.SetPermissions(true);
            var minPermissions = RoleService.SetPermissions(false);
            minPermissions.Information.AuditTrail.Enabled = true;

            requiredRoles.Add(new KeyValuePair<string, Permissions>(Role.Administrator, maxPermissions));
            requiredRoles.Add(new KeyValuePair<string, Permissions>(Role.Guest, minPermissions));

            var existingRoles = _context.Roles.Where(r => r.Name != null)
                .ToDictionary(_ => _.Name, _ => _);

            foreach (var requiredRole in requiredRoles)
            {
                if (!existingRoles.TryGetValue(requiredRole.Key, out Role role))
                {
                    var roleToAdd = new Role
                    {
                        Name = requiredRole.Key,
                        Permissions = requiredRole.Value
                    };

                    _context.Roles.Add(roleToAdd);
                }
                else
                {
                    role.Permissions = requiredRole.Value;
                }
            }

            _context.SaveChanges();
        }

        private static void SeedUsers(AppDbContext _context)
        {
            if (!_context.Users.Any())
            {
                var users = new(string UserName, string Email, string DisplayName, string Password, string Role, bool IsActive)[]
                {
                        ("AdminUser", "admin@sauter.ch", "admin", "AdminUser!", Role.Administrator, true),
                        ("GuestUser", null, "guest", "GuestUser!", Role.Guest, true)
                };

                foreach (var userToHash in users)
                {
                    var passwordSalt = CryptographyService.PasswordSaltInBase64();
                    var passwordHash = CryptographyService.PasswordToHashBase64(userToHash.Password, passwordSalt);

                    var user = new User
                    {
                        UserName = userToHash.UserName,
                        Email = userToHash.Email,
                        DisplayName = userToHash.DisplayName,
                        PasswordHash = passwordSalt,
                        PasswordSalt = passwordHash,
                        Role = _context.Roles.FirstOrDefault(r => r.Name == userToHash.Role),
                        IsActive = userToHash.IsActive
                    };

                    _context.Users.Add(user);
                }
            }

            foreach (var user in _context.Users)
            {
                if (user.RoleId == null)
                {
                    var minPermissions = RoleService.SetPermissions(false);
                    minPermissions.Information.AuditTrail.Enabled = true;

                    var guestRole = _context.Roles.FirstOrDefault(r => r.Permissions.Equals(minPermissions));
                    user.RoleId = guestRole.Id;

                    if (user.UserName == Role.Administrator)
                    {
                        var adminRole = _context.Roles.FirstOrDefault(a => a.Name == Role.Administrator);
                        user.RoleId = adminRole.Id;
                    }
                }
            }
        }
    }
}
