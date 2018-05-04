using Entities = Test.Data.Entities;

namespace Test.WebAPI.Swagger.RealData.Services
{
    public class RoleService
    {
        public static Entities.Permissions SetPermissions(bool enable)
        {
            var permissions = new Entities.Permissions();
            permissions.Information.AuditTrail.Enabled = enable;
            permissions.Settings.UsersAndRoles.Users.Enabled = enable;
            permissions.Settings.UsersAndRoles.Roles.Enabled = enable;
            permissions.Settings.NetworkSettings.EmailServerSettings.Enabled = enable;
            permissions.Settings.NetworkSettings.SMSServerSettings.Enabled = enable;
            permissions.Settings.Organisations.Enabled = enable;
            return permissions;
        }
    }
}
